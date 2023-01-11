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
using ScriptureExercise.Models;
using Common.Enums;

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
            var result = new ApiResponseBody();

            var createMemberInput = new CreateMember_Input
            {
                Account = request.Account,
                Password = request.Password,
            };

            var createMemberOutput = memberService.CreateMember(createMemberInput);
            if (createMemberOutput.IsFail)
            {
                result.Status = ApiOperationStatus.DataRequireUnique;
                result.Message = createMemberOutput.FailMessage;
                return Ok(result);
            }

            //註冊完自動登入
            var issueClaimsInput = new IssueClaimsInput
            {
                Account = createMemberOutput.Payload.Account,
                Member = createMemberOutput.Payload.Member,
            };
            await accountService.IssueClaims(issueClaimsInput);

            result.Message = "註冊成功，並已將您登入，3秒後自動轉址";
            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(CreateMemberPostModel request)
        {
            var result = new ApiResponseBody();

            var input = new CreateMember_Input
            {
                Account = request.Account,
                Password = request.Password,
            };
            var output_WithPayload = memberService.GetMember_ByInput(input);
            if (output_WithPayload.IsFail)
            {
                result.Status = ApiOperationStatus.DataNotFound;
                result.Message = output_WithPayload.FailMessage;
                return Ok(result);
            }

            var memberFound = output_WithPayload.Payload;
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
                Member = memberFound,
            };
            await accountService.IssueClaims(issueClaimsInput);

            result.Message = "登入成功";
            return Ok(result);
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
        //        return Ok("創建會員失敗：" + createMemberOutput.ErrMsg);
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
        //        return Ok("您輸入的密鑰對應不上任何會員。請檢查是否打錯，或考慮創建新的會員密鑰");
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
        //        return Ok("綁定會員失敗：" + output.ErrMsg);
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
