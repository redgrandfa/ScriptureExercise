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
        //private readonly IMemberService memberService;
        public AccountController(
            IAccountService accountService
            //,IMemberService memberService
            )
        {
            this.accountService = accountService;
            //this.memberService = memberService;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        //讓 各顆超連結打到 這
        [HttpGet("{scheme}")]
        public IActionResult LoginOption([FromRoute] string scheme)
        {
            //設定驗證選項
            var properties = new AuthenticationProperties()
            {
                //【驗證】完，要重新導向到的網址
                RedirectUri = Url.Action(nameof(HandleResponse)),
            };

            //用指定的scheme 發起挑戰(=要你去做驗證)
            return Challenge(properties, scheme);
        }
        //會導到 {第三方} 對應的clientID、ClientSecret的【你創的對應的小App】(會有標題等資訊)

        //User要在這個小App 做
        //1. {第三方} 的驗證
        //2. 登入後，User【同意/授權】這個小App，將 同意使用的個資 帶著回到【小App裡設定的CallBackUrl】
        //回到CallBackUrl後，挑戰結束了，重導到 驗證選項設定的RedirectUri

        public async Task<IActionResult> HandleResponse() // 挑戰完的 重導到此
        {
            //將 request請求 驗證
            await HttpContext.AuthenticateAsync( CookieAuthenticationDefaults.AuthenticationScheme );

            //var claims =
            //User.Claims.Select(claim => new
            //{
            //    claim.Issuer,
            //    claim.OriginalIssuer,
            //    claim.Type,
            //    claim.Value_T,
            //});

            //用JSON格式觀察測試結果
            //return Json(claims);
            //return Json(User.Claims);

            //網頁畫面出現後，到開發工具中會發現已有cookie，表示已經完成了cookie驗證


            var input = new GetAccountInput
            {
            };


            var output = accountService.GetAccount(input);
            if ( output.OperationResult == null ){
                return View("UnTrackedAccount" /*, new UnTrackedAccountVM()*/ );
            }

            var issueClaimsInput = new IssueClaimsInput
            {
                Account = output.OperationResult
            };
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await accountService.IssueClaims(issueClaimsInput);

            return Redirect("/");
        }


        public IActionResult UnTrackedAccount() 
        { 
            return View(new CreateMemberFormModel() ); 
        }


        /// <summary>
        /// 可登出 本網會員/第三方帳號
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
