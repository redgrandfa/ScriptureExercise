using Common.DBModels;
using Common.Enums;
using Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScriptureExercise.Models;
using ScriptureExercise.Models.AccountVM;
using ScriptureExercise.Models.MemberVM;
using ScriptureExercise.Services;
using System;

namespace ScriptureExercise.Controllers.WebAPI
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Member")]
    public class ApiMemberController : ControllerBase
    {
        //private readonly IAccountService accountService;
        private readonly IMemberService memberService;
        public ApiMemberController(
            //IAccountService accountService,
            IMemberService memberService
            )
        {
            //this.accountService = accountService;
            this.memberService = memberService;
        }

        [HttpPost]
        public IActionResult UpdateName(MemberEditVM request)
        {
            Action<Member> action = member =>
            {
                member.Value.Name = request.Name;
            };
            return UpdateByCondition(action, "修改名稱成功");
        }

        [HttpPost]
        public IActionResult UpdateAccount(CreateMemberPostModel request)
        {
            var result = new ApiResponseBody();

            var output = memberService.UpdateAccount(request.Account);
            if (output.IsFail)
            {
                result.Status = ApiOperationStatus.DataRequireUnique;
                result.Message = output.FailMessage;
                return Ok(result);
            }

            result.Message = "修改帳號成功";
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpdatePassword(CreateMemberPostModel request)
        {
            Action<Member> action = member =>
            {
                member.Value.Password = Encryption.SHA256(request.Password);
            };
            return UpdateByCondition(action, "修改密碼成功");
        }


        [HttpGet]
        public IActionResult GetSubjectCollectList() //背景API
        {
            var result = new ApiResponseBody();

            var member = memberService.GetCurrentMember();
            result.Payload = member.Value.SubjectCollectedList;
            result.Message = "成功取得考科收藏資料";
            return Ok(result);
        }

        [HttpPost]
        public IActionResult ToggleSubjectCollect(ToggleSubjectCollectRequest request)
        {
            var subjectCode = request.SubjectCode;

            Action<Member> action;
            string successMsg;

            if (request.CollectStatus)
            {
                action = (member) =>
                {
                    var list = member.Value.SubjectCollectedList;
                    list.Remove(subjectCode);
                };
                successMsg = "已自書單移除";
            }
            else
            {
                action = (member) =>
                {
                    var list = member.Value.SubjectCollectedList;
                    list.Add(subjectCode);
                };
                successMsg = "已加入書單";
            }

            //不能在Action裡條件判斷...實質型別 會訂死 順序無法影響? 實質/參考型別...

            return UpdateByCondition(action, successMsg);
        }

        [NonAction]
        public IActionResult UpdateByCondition(Action<Member>  action,  string successMsg)
        {
            var result = new ApiResponseBody();

            var output = memberService.UpdateMember(action);
            if (output.IsFail)
            {
                result.Status = ApiOperationStatus.DataNotFound;
                result.Message = output.FailMessage;
                return Ok(result);
            }

            result.Message = successMsg;
            return Ok(result);
        }
    }
}
