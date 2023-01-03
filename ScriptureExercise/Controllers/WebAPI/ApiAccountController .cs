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
    public class ApiAccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly IMemberService memberService;
        public ApiAccountController(
            IAccountService accountService ,
            IMemberService memberService
            )
        {
            this.accountService = accountService;
            this.memberService = memberService;
        }

        //潛規則：本網帳號一定在第一個accountList中

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(CreateMemberPostModel request)
        {
            var createMemberInput = new CreateMember_Input
            {
                Account = request.Account,
                Password = request.Password,
            };

            var createMemberOutput = memberService.CreateMember(createMemberInput);
            if (createMemberOutput.IsFail)
            {
                return BadRequest("創建會員失敗：" + createMemberOutput.FailMessage);
            }

            //註冊完自動登入
            var issueClaimsInput = new IssueClaimsInput
            {
                Account = createMemberOutput.Payload,
            };
            await accountService.IssueClaims(issueClaimsInput);

            return Ok("註冊成功，並登入完成");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(CreateMemberPostModel request)
        {
            var input = new CreateMember_Input
            {
                Account = request.Account,
                Password = request.Password,
            };
            var memberFound = memberService.GetMember_ByInput(input);
            
            if (memberFound == null)
            {
                return BadRequest("登入會員失敗：" + "找不到此組帳密");
            }

            var issueClaimsInput = new IssueClaimsInput
            {
                Account = new Account
                {
                    PK = memberFound.Value.AccountPK_List[0],
                    Value = new Account.Value_T
                    {
                        FK_Member = memberFound.PK,
                    },
                },
            };
            await accountService.IssueClaims(issueClaimsInput);
            return Ok("登入成功");
        }




        //[HttpPost]
        //[AllowAnonymous]
        /// <summary>
        /// 還沒有會員 => 先創會員再綁第三方帳號
        /// </summary>
        //public async Task<IActionResult> CreateMemberAsync(CreateMemberPostModel request)
        //{
        //    var createMemberInput = new CreateMember_Input
        //    {
        //        Account = request.Account,
        //        Password = request.Password,
        //    };

        //    var createMemberOutput = memberService.CreateMember(createMemberInput);
        //    if (!createMemberOutput.OperationResult)
        //    {
        //        return BadRequest("創建會員失敗：" + createMemberOutput.ErrMsg);
        //    }

        //    //繼續創建 account > 綁起來 
        //    return await CreateAccountAsync(createMemberOutput.MemberCreated.PK);
        //}

        //[HttpPost]
        //[AllowAnonymous]
        /// <summary>
        /// 已有會員身分 => 創新帳號綁 既有會員。   
        /// </summary>
        /// <remarks>
        /// 對用戶而言：我的帳號，綁到會員身分上
        /// 對後端而言：創一筆Acoount資料
        /// </remarks>
        //public async Task<IActionResult> BindMemberAsync(CreateMemberPostModel request)
        //{
        //    Member.PK_T memberPK = memberService.GetMemberPK_ByBindKey(request.BindKey);
        //    if (memberPK == null)
        //    {
        //        return BadRequest("您輸入的密鑰對應不上任何會員。請檢查是否打錯，或考慮創建新的會員密鑰");
        //    }

        //    return await CreateAccountAsync(memberPK);
        //}

        //[NonAction]
        //public async Task<IActionResult> CreateAccountAsync(Member.PK_T memberPK)
        //{

        //    var input = new CreateAccount_Input
        //    {
        //        MemberPK = memberPK,
        //    };
        //    var output = accountService.CreateAccount(input);
        //    if (!output.OperationResult)
        //    {
        //        return BadRequest("綁定會員失敗：" + output.ErrMsg);
        //    }

        //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    var issueClaimsInput = new IssueClaimsInput
        //    {
        //        Account = output.AccountCreated,
        //    };
        //    await accountService.IssueClaims(issueClaimsInput);

        //    return Ok("成功");
        //}
    }
}
