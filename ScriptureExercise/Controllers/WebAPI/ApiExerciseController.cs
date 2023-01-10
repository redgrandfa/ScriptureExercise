using Common.DBModels;
using Common.DTOModels.ExcerciseDTOs;
using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScriptureExercise.Models;
using ScriptureExercise.Models.ExcerciseVM;
using ScriptureExercise.Services;
using System;

namespace ScriptureExercise.Controllers.WebAPI
{
    [Authorize(Roles = "Member")]
    public class ApiExerciseController : AppApiControllerBase
    {
        private readonly IExerciseService exerciseService;
        private readonly IMemberService memberService;
        public ApiExerciseController(
            IExerciseService exerciseService
            , IMemberService memberService
            )
        {
            this.exerciseService = exerciseService;
            this.memberService = memberService;
        }

        //紀錄答題狀況
        [HttpPost]
        public IActionResult PostPaper(PostPaperRequestModel request)
        {
            var result = new ApiResponseBody();

            //var now = DateTime.UtcNow;
            var now = DateTime.Now;

            var input = request.RecordCreate;
            input.CreateTime = now;
            var output_withCreateTimeId = exerciseService.CreateExerciseRecord(input);

            if (output_withCreateTimeId.IsFail)
            {
                result.Status = ApiOperationStatus.DataRequireUnique;
                result.Message = output_withCreateTimeId.FailMessage;
                return Ok(result);
            }


            Action<Member> action = (member) =>
            {
                var t = request.MemberUpdate;

                member.Value.ChoicesQuestion_Done += t.ChoicesQuestion_Done;
                member.Value.ChoicesQuestion_Correct += t.ChoicesQuestion_Correct;

                member.Value.EssayQuestion_Done += t.EssayQuestion_Done;
                member.Value.EssayQuestion_Correct += t.EssayQuestion_Correct;

                member.Value.BlankFillQuestion_Done += t.BlankFillQuestion_Done;
                member.Value.BlankFillQuestion_Correct += t.BlankFillQuestion_Correct;

                member.Value.ExerciseRecordCreateTimeId_List.Add(now.ToString("yyMMddHHmmss"));
            };
            var output = memberService.UpdateMember(action);
            if (output.IsFail)
            {
                result.Status = ApiOperationStatus.DataNotFound;
                result.Message = output.FailMessage;
                return Ok(result);
            }

            //確保member的紀錄數量
            var member = memberService.GetCurrentMember();
            if (member.Value.ExerciseRecordCreateTimeId_List.Count > 15)
            {
                var createId = member.Value.ExerciseRecordCreateTimeId_List[0];
                exerciseService.DeleteExerciseRecord(createId);
            }

            result.Message = "批改完畢";
            result.Payload = output_withCreateTimeId.Payload;
            return Ok(result);
        }

        public IActionResult GetRecordList()
        {
            var result = new ApiResponseBody();

            var output = exerciseService.GetExerciseRecordList();
            if (output.IsFail)
            {
                result.Status= ApiOperationStatus.DataNotFound;
                result.Message = output.FailMessage;
                return Ok(result);
            }

            result.Payload = output.Payload;
            return Ok(result);
        }

        [HttpGet("{createTimeId}")]
        public IActionResult GetRecord(string createTimeId)
        {
            var result = new ApiResponseBody();

            var output = exerciseService.GetExerciseRecord(createTimeId);

            if (output.IsFail)
            {
                result.Status = ApiOperationStatus.DataNotFound;
                result.Message = output.FailMessage;
                return Ok(result);
            }

            result.Payload = output.Payload;
            result.Message = "紀錄取得成功";
            return Ok(result);
        }

        [HttpPost("{createTimeId}")]
        public IActionResult DeleteRecord(string createTimeId)
        {
            var result = new ApiResponseBody();
            //if (createTimeId == null)
            //{
            //    return Ok("紀錄編號不可為空");
            //}

            var output = exerciseService.DeleteExerciseRecord(createTimeId);
            if (output.IsFail)
            {
                result.Status = ApiOperationStatus.DataNotFound;
                result.Message = output.FailMessage; //"刪除紀錄失敗" + output.FailMessage
                return Ok(result);
            }

            result.Message = "刪除紀錄成功";
            return Ok(result);
        }
    }
}
