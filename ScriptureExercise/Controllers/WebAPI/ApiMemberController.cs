using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScriptureExercise.Services;
using ScriptureExercise.Models.MemberVM;

namespace ScriptureExercise.Controllers.WebAPI
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Member")]
    public class ApiMemberController : ControllerBase
    {
        private readonly IMemberService memberService;

        public ApiMemberController(IMemberService memberService)
        {
            this.memberService = memberService;
        }

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
    }
}
