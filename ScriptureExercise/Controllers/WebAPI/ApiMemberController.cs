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
                return BadRequest(output.FailMessage);
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

        [HttpPost]
        public IActionResult UpdateScripture(MemberEditVM request)
        {
            Action<Member > action = (member)=>
            {
                member.Value.ScriptureShowList = request.ScriptureShowList;
            };
            return UpdateByCondition(action, "修改經典顯示成功");
        }

        [HttpPost]
        public IActionResult ToggleSubjectCollect(MemberEditVM request)
        {
            Action<Member> action = (member) =>
            {
                var list = member.Value.SubjectCollectedList;
                var target = 1;
                if (list.Contains(target))
                {
                    list.Remove(target);
                }
                else
                {
                    list.Add(target);
                }
            };
            return UpdateByCondition(action, "修改經典顯示成功");
        }

        [NonAction]
        public IActionResult UpdateByCondition(Action<Member>  action,  string successMsg)
        {
            var output = memberService.UpdateMember(action);
            if (output.IsFail)
            {
                return BadRequest(output.FailMessage);
            }
            return Ok(successMsg);
        }
    }
}
