using Common.DBModels;
using Common.DTOModels.ExcerciseDTOs;
using Common.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ScriptureExercise.Services
{
    public interface IExerciseService
    {

        //紀錄 相關方法
        CreateExerciseRecordOutput CreateExerciseRecord(CreateExerciseRecordInput input);
        GetRecordListOutput GetExerciseRecordList();
        GetRecordOutput GetExerciseRecord(string createTimeId);
        DeleteRecordOutput DeleteExerciseRecord(string createTimeId);
    }
    public class ExamService: BaseService , IExerciseService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMemoryCacheRepository cacheRepo;
        private readonly IMemberService memberService;

        public ExamService(
            IHttpContextAccessor httpContextAccessor
            , IMemoryCacheRepository cacheRepo
            , IMemberService memberService
            ) : base(httpContextAccessor, cacheRepo)
        {
            this.memberService = memberService;
        }

        public CreateExerciseRecordOutput CreateExerciseRecord(CreateExerciseRecordInput input)
        {
            var result = new CreateExerciseRecordOutput();
            var memberId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);

            try
            {
                var exerciseRecord = new ExerciseRecord
                {
                    PK = new ExerciseRecord.PK_T
                    {
                        FK_Member = new Member.PK_T
                        {
                            MemberId = memberId,
                        },
                        CreateTimeId = input.CreateTime.ToString("yyMMddHHmmss"),
                    },
                    Value = new ExerciseRecord.Value_T
                    {
                        FK_JsonFileName = input.ExerciseJsonFileName,
                        //PaperName = input.PaperName,
                        ReplyJSON = input.ReplyJSON,
                        Score = input.Score,
                        PercentScore = input.PercentScore
                    },
                };
                _cacheRepo.Set(exerciseRecord.GetRedisKeyString() , exerciseRecord.Value );

                result.OperationResult = exerciseRecord.PK.CreateTimeId;
            }
            catch(Exception ex)
            {
                result.ErrMsg = $"紀錄練習結果失敗。{ex}";
            }

            return result;
        }

        public GetRecordListOutput GetExerciseRecordList()
        {
            var result = new GetRecordListOutput();

            var member = memberService.GetCurrentMember();
            result.OperationResult =
                member.Value.ExerciseRecordCreateTimeId_List.Select(RecordCreateTime =>
                {
                    var exerciseRecord = new ExerciseRecord
                    {
                        PK = new ExerciseRecord.PK_T
                        {
                            CreateTimeId = RecordCreateTime,
                            FK_Member = member.PK,
                        },
                    };

                    exerciseRecord.Value = _cacheRepo.Get<ExerciseRecord.Value_T>(
                        exerciseRecord.GetRedisKeyString());

                    return new RecordRow
                    {
                        CreateTime = RecordCreateTime,
                        ExerciseJsonFileName = exerciseRecord.Value.FK_JsonFileName,
                        PercentScore = exerciseRecord.Value.PercentScore,
                    };
                }).ToList();

            return result;
        }

        public GetRecordOutput GetExerciseRecord(string createTimeId)
        {
            var result = new GetRecordOutput();

            var exerciseRecord = new ExerciseRecord
            {
                PK = GetRecordPK_ById(createTimeId),
            };

            exerciseRecord.Value = _cacheRepo.Get<ExerciseRecord.Value_T>(
                exerciseRecord.GetRedisKeyString());


            result.OperationResult = exerciseRecord.Value;

            return result;
        }

        public DeleteRecordOutput DeleteExerciseRecord(string createTimeId)
        {
            var result = new DeleteRecordOutput();
            
            var exerciseRecord = new ExerciseRecord
            {
                PK = GetRecordPK_ById(createTimeId),
            };

            try
            {
                var key = exerciseRecord.GetRedisKeyString();
                exerciseRecord.Value = _cacheRepo.Get<ExerciseRecord.Value_T>(key);

                if (exerciseRecord.Value == null)
                {
                    result.ErrMsg = $"編號：【{createTimeId}】這筆紀錄不存在";
                    return result;
                }

                _cacheRepo.Remove(key);

                Action<Member> action = member =>
                {
                    member.Value.ExerciseRecordCreateTimeId_List.Remove(createTimeId);
                };
                memberService.UpdateMember(action);



                result.OperationResult = true;
            }
            catch(Exception ex)
            {
                result.ErrMsg = ex.Message;
            }

            return result;
        }

        private ExerciseRecord.PK_T GetRecordPK_ById(string createTimeId)
        {
            var memberId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
            var exerciseRecordPK = new ExerciseRecord.PK_T
            {
                CreateTimeId = createTimeId,
                FK_Member = new Member.PK_T
                {
                    MemberId = memberId,
                }
            };
            return exerciseRecordPK;
        }
    }
}
