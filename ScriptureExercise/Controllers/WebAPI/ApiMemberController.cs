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

namespace ScriptureExercise.Controllers.WebAPI
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Member")]
    public class ApiMemberController : ControllerBase
    {
        //private readonly IAccountService accountService;
        //private readonly IMemberService memberService;
        //public ApiMemberController(
        //    IAccountService accountService ,
        //    IMemberService memberService
        //    )
        //{
        //    this.accountService = accountService;
        //    this.memberService = memberService;
        //}

        [HttpPost]
        public IActionResult Update(MemberUpdateRequestModel request)
        {
            //姓名
            //密鑰
            //偏好
            //顯示在經典選單 中的一部分 => filter

            //var input = new
            //var output = memberService.Update(input);
            //if (output) {
            //
            //}

            return Ok("修改完成");
        }



        //[HttpPost]
        //public IActionResult PostPaper()
        //{
        //    var memberId = int.Parse(User.Identity.Name);

        //    //PDF? html 只看錯誤題 篩選
        //    // 每一題盼對錯 才能計分
        //    //資料化

        //    return Redirect("");
        //}
    }
}
