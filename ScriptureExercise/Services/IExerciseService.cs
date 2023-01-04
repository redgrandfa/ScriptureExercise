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
    public class ExerciseService: BaseService , IExerciseService
    {
        private readonly IMemberService memberService;
        public ExerciseService(
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
            var memberId = memberService.GetCurrentMemberId();

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

            bool isSuccess = _cacheRepo.Create(exerciseRecord.GetRedisKeyString() , exerciseRecord.Value );
            if (!isSuccess)
            {
                result.FailMessage = "記錄批改結果失敗";
                return result;
            }

            result.Payload = exerciseRecord.PK.CreateTimeId;
            return result;
        }

        public GetRecordListOutput GetExerciseRecordList()
        {
            var result = new GetRecordListOutput();

            var member = memberService.GetCurrentMember();
            if (member == null)
            {
                result.FailMessage = "取紀錄列表失敗";
                return result;
            }

            result.Payload = 
                member.Value.ExerciseRecordCreateTimeId_List
                .Select(RecordCreateTime =>
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

            if(exerciseRecord.Value == null)
            {
                result.FailMessage = "紀錄不存在";
                return result;
            }

            result.Payload = exerciseRecord.Value;
            return result;
        }

        public DeleteRecordOutput DeleteExerciseRecord(string createTimeId)
        {
            var result = new DeleteRecordOutput();
            
            var exerciseRecord = new ExerciseRecord
            {
                PK = GetRecordPK_ById(createTimeId),
            };

            var key = exerciseRecord.GetRedisKeyString();
            exerciseRecord.Value = _cacheRepo.Get<ExerciseRecord.Value_T>(key);

            if (exerciseRecord.Value == null)
            {
                result.FailMessage = "異常：此筆記錄不存在";//$"編號：【{createTimeId}】這筆紀錄不存在";
                return result;
            }

            _cacheRepo.Remove(key);

            Action<Member> action = member =>
            {
                member.Value.ExerciseRecordCreateTimeId_List.Remove(createTimeId);
            };
            var updateMemberOutput =  memberService.UpdateMember(action);
            if (updateMemberOutput.IsFail)//理論上應該要拋例外...?
            {
                result.FailMessage = updateMemberOutput.FailMessage;
                return result;
            }

            return result;
        }

        private ExerciseRecord.PK_T GetRecordPK_ById(string createTimeId)
        {
            var memberId = memberService.GetCurrentMemberId();
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
