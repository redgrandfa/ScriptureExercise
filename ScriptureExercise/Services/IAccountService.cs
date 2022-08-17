using Common.DBModels;
using Common.DTOModels.AccountDTOs;
using Common.DTOModels.MemberDTOs;
using Common.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ScriptureExercise.Services
{
    public interface IAccountService
    {
        GetAccountOutput GetAccount(GetAccountInput input);
        /// <summary>
        /// 以輸入 查到member後，創Account回傳
        /// </summary>
        /// <param name="input">Member主鍵、。</param>
        /// <returns>建好的Account</returns>
        CreateAccount_Output CreateAccount(CreateAccount_Input input);
        Task IssueClaims(IssueClaimsInput input);
    }

    public class AccountService : BaseService, IAccountService
    {
        public AccountService(
            IHttpContextAccessor httpContextAccessor, 
            IMemoryCacheRepository cacheRepo) 
            :base(httpContextAccessor, cacheRepo)
        {}

        public GetAccountOutput GetAccount(GetAccountInput input)
        {
            var result = new GetAccountOutput();


            //以 第三方的provider + nameId，查登入途徑 是否已有
            var idClaim = _httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            Account account = new Account
            {
                PK = new Account.PK_T
                {
                    Provider = idClaim.Issuer,
                    AccountId_FromProvider = idClaim.Value,
                },
            };

            account.Value = _cacheRepo.Get<Account.Value_T>( account.GetRedisKeyString() );


            //沒查到 => 代表第一次用 這第三方帳號登入 => 要回網站 輸入/設定金鑰
            if (account.Value == null)
            {
                //result.OperationResult = null;
                return result;
            }

            //有查到 => 一定有 會員身分
            //Member member = new Member
            //{
            //    PK = accountValueFound.FK_Member,
            //};
            //string memberKey = member.GetRedisKeyString();
            //var memberValueFound = _cacheRepo.Get<Member.Value_T>(memberKey);

            result.OperationResult = account;
            return result;
        }

        public CreateAccount_Output CreateAccount(CreateAccount_Input input)
        {
            var result = new CreateAccount_Output();

            var idClaim = _httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);


            //創Account資料、member下的一對多+一筆
            try
            {
                Account account = new Account
                {
                    PK = new Account.PK_T
                    {
                        Provider = idClaim.Issuer,
                        AccountId_FromProvider = idClaim.Value,
                    },
                    Value = new Account.Value_T
                    {
                        CreateTime = DateTime.UtcNow,
                        UpdateTime = DateTime.UtcNow,
                        //CreateMemberId = memberId,
                        //UpdateMemberId = memberId,

                        FK_Member = input.MemberPK,
                    },
                };
                _cacheRepo.Set(account.GetRedisKeyString(), account.Value);


                Member member = new Member
                {
                    PK = input.MemberPK,
                };
                string memberKey = member.GetRedisKeyString();
                member.Value = _cacheRepo.Get<Member.Value_T>(memberKey);
                member.Value.AccountPK_List.Add(account.PK);
                member.Value.UpdateTime = DateTime.UtcNow;

                _cacheRepo.Set( memberKey, member.Value);


                result.AccountCreated = account;
                result.OperationResult = true;
            }
            catch (Exception ex)
            {
                result.ErrMsg = ex.Message;
                return result;
            }

            return result;
            //return Task.FromResult(result); //同步方法裡可以把Task拿出來?
        }

        public async Task IssueClaims( IssueClaimsInput input)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, input.Account.Value.FK_Member.MemberId.ToString() ),
                new Claim("LoginThrough", input.Account.PK.Provider ),

                new Claim(ClaimTypes.Role , "Member"), //區分第三方登入
            };


            //用 聲明集合，造一個ClaimsIdentity物件。
            //(各項資訊 組成一張證件 的概念)
            var claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme); //其實只是個字串 "Cookies"

            //用ClaimsIdentity物件，造一個ClaimsPrincipal物件
            //(證件 造出 人的身分概念)
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            //先設定驗證的屬性
            var authProperties = new AuthenticationProperties
            {
                //舉幾個例，可參考官方文件AuthenticationProperties類別
                //AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(44), //有設才不會關掉網頁就自動消失?
                IsPersistent = true,
            };

            //將此ClaimsPrincipal登入。登入方法，會創造一個cookie
            await  _httpContextAccessor.HttpContext.SignInAsync(
                    //CookieAuthenticationDefaults.AuthenticationScheme,  //看一下預設的 scheme計畫 是
                    claimsPrincipal,
                    authProperties);
        }
    }
}
