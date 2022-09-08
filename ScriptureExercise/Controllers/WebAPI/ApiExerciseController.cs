using Common.DBModels;
using Common.DTOModels.ExcerciseDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScriptureExercise.Models.ExcerciseVM;
using ScriptureExercise.Services;
using System;

namespace ScriptureExercise.Controllers.WebAPI
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Member")]
    public class ApiExerciseController : ControllerBase
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

        [HttpPost]
        //紀錄答題狀況
        public IActionResult PostPaper(PostPaperRequestModel request)
        {
            //var memberId = int.Parse(User.Identity.Name);
            //var now = DateTime.UtcNow;
            var now = DateTime.Now;

            var input = request.RecordCreate;
            input.CreateTime = now;
            var output = exerciseService.CreateExerciseRecord(input);
            if (output.OperationResult == null)
            {
                return BadRequest(output.ErrMsg);
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

                //確保member的紀錄數量
                member.Value.ExerciseRecordCreateTimeId_List.Add(now.ToString("yyMMddHHmmss"));
                if (member.Value.ExerciseRecordCreateTimeId_List.Count > 15)
                {
                    member.Value.ExerciseRecordCreateTimeId_List.RemoveAt(0);
                }
            };
            var updateMemberOutput = memberService.UpdateMember(action);
            if (!updateMemberOutput.OperationResult)
            {
                return BadRequest(updateMemberOutput.ErrMsg);
            }

            return Ok(output.OperationResult);
        }

        public IActionResult GetRecordList()
        {
            var output = exerciseService.GetExerciseRecordList();
            if (output.OperationResult == null)
            {
                return BadRequest("取紀錄列表失敗" + output.ErrMsg);
            }
            return Ok(output.OperationResult);
        }

        [HttpGet("{createTimeId}")]
        public IActionResult GetRecord(string createTimeId)
        {
            var output = exerciseService.GetExerciseRecord(createTimeId);
            if (output.OperationResult == null)
            {
                return BadRequest("取紀錄失敗" + output.ErrMsg);
            }
            return Ok(output.OperationResult);
        }

        [HttpPost("{createTimeId}")]
        public IActionResult DeleteRecord(string createTimeId)
        {
            if (createTimeId == null)
            {
                return BadRequest("紀錄編號不可為空");
            }

            var output = exerciseService.DeleteExerciseRecord(createTimeId);
            if (!output.OperationResult)
            {
                return UnprocessableEntity("刪除紀錄失敗" + output.ErrMsg);
            }
            return Ok("刪除紀錄成功");
        }
    }
}
