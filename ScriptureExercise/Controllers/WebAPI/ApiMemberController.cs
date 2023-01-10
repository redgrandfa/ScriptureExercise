using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScriptureExercise.Services;
using ScriptureExercise.Models.MemberVM;
using Common.DBModels;
using Common.DTOModels.AccountDTOs;
using Common.DTOModels.MemberDTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using ScriptureExercise.Models.AccountVM;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Common.Repositories;
using System;
using ScriptureExercise.Models;
using Common.Enums;

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
            var output = memberService.UpdateAccount(request.Account);
            if (output.IsFail)
            {
                return Ok(output.FailMessage);
            }
            return Ok("修改帳號成功");
        }
        [HttpPost]
        public IActionResult UpdatePassword(CreateMemberPostModel request)
        {
            Action<Member> action = member =>
            {
                member.Value.Password = request.Password;
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
                successMsg = "已取消收藏";
            }
            else
            {
                action = (member) =>
                {
                    var list = member.Value.SubjectCollectedList;
                    list.Add(subjectCode);
                };
                successMsg = "收藏成功";
            }

            //不能在Action裡條件判斷...實質型別 會訂死 順序無法影響? 實質/參考型別...

            return UpdateByCondition(action, successMsg);
        }


        [HttpPost]
        [Obsolete("原本是收藏經典，且以int表示")]
        public IActionResult UpdateScripture(MemberEditVM request)
        {
            Action<Member > action = (member)=>
            {
                member.Value.ScriptureShowList = request.ScriptureShowList;
            };
            return UpdateByCondition(action, "修改經典顯示成功");
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
