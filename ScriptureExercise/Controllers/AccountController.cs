using Common.DBModels;
using Common.DTOModels.AccountDTOs;
using Common.DTOModels.MemberDTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScriptureExercise.Models.AccountVM;
using ScriptureExercise.Services;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ScriptureExercise.Controllers
{
    [Route("~/[controller]/[action]")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;
        private readonly IMemberService memberService;
        public AccountController(
            IAccountService accountService            
            ,IMemberService memberService
        )
        {
            this.accountService = accountService;
            this.memberService = memberService;
        }

        /// <summary>
        /// 登入或註冊 共用同一個View
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }


        //[AllowAnonymous]
        ////讓 各顆超連結打到 這
        //[HttpGet("{scheme}")]
        //public IActionResult LoginOption([FromRoute] string scheme)
        //{
        //    var properties = new AuthenticationProperties()
        //    {
        //        //【驗證】完，要重新導向到的網址
        //        RedirectUri = Url.Action(nameof(HandleResponse)),
        //    };

        //    return Challenge(properties, scheme);
        //}

        //public async Task<IActionResult> HandleResponse() // 挑戰完的 重導到此
        //{
        //    //將 request請求 驗證
        //    await HttpContext.AuthenticateAsync( CookieAuthenticationDefaults.AuthenticationScheme );

        //    //var claims =
        //    //User.Claims.Select(claim => new
        //    //{
        //    //    claim.Issuer,
        //    //    claim.OriginalIssuer,
        //    //    claim.Type,
        //    //    claim.Value_T,
        //    //});

        //    //用JSON格式觀察測試結果
        //    //return Json(claims);
        //    //return Json(User.Claims);

        //    //網頁畫面出現後，到開發工具中會發現已有cookie，表示已經完成了cookie驗證


        //    var input = new GetAccountInput
        //    {
        //    };


        //    var output = accountService.GetAccount(input);
        //    if ( output.OperationResult == null ){
        //        return View("UnTrackedAccount" /*, new UnTrackedAccountVM()*/ );
        //    }

        //    var issueClaimsInput = new IssueClaimsInput
        //    {
        //        Account = output.OperationResult
        //    };
        //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    await accountService.IssueClaims(issueClaimsInput);

        //    return RedirectToAction("List","Eercise");
        //}


        //public IActionResult UnTrackedAccount() 
        //{ 
        //    return View(new CreateMemberPostModel() ); 
        //}


        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return Content("權限不足");
        }
    }
}
