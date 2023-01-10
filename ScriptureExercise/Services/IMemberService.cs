using Common.DBModels;
using Common.DTOModels;
using Common.DTOModels.MemberDTOs;
using Common.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ScriptureExercise.Services
{
    public interface IMemberService
    {
        CreateMember_Output CreateMember(CreateMember_Input input);
        UpdateMember_Output UpdateMember(Action<Member> action);
        //DeleteMember_Output DeleteMember(Member.PK_T memberPK);

        UpdateAccount_Output UpdateAccount(string account);


        BaseOutput_withPayload<Member> GetMember_ByInput(CreateMember_Input input);
        int GetCurrentMemberId();
        Member GetCurrentMember();
        Member GetMember_ById(int memberId);
    }
    public class MemberService : BaseService, IMemberService
    {
        public MemberService(
            IHttpContextAccessor httpContextAccessor,
            IMemoryCacheRepository cacheRepo
        ): base(httpContextAccessor, cacheRepo)
        {}

        public CreateMember_Output CreateMember(CreateMember_Input input)
        {
            var result = new CreateMember_Output();


            var getMember_output = GetMember_ByInput(input);

            //檢查 已存在衝突
            if (getMember_output.IsSuccess)
            {
                result.FailMessage = "此會員帳號已被其他人佔用，請換一個";
                return result;
            }


            //創建會員
            //名字 可能從第三方取現成的
            var name = _httpContextAccessor.HttpContext.User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value; ;
            if (name == null )
            {
                name = "";
            }
                    
            var counter = _cacheRepo.Get<EntityCounter>(nameof(EntityCounter));

            try
            {
                var account = new Account
                {
                    PK = new Account.PK_T
                    {
                        Provider = "Cookie",
                        AccountId_FromProvider = input.Account,
                    },
                    Value = new Account.Value_T
                    {
                        CreateTime = DateTime.UtcNow,
                        UpdateTime = DateTime.UtcNow,
                    }
                };

                var member = new Member
                {
                    PK = new Member.PK_T
                    {
                        MemberId = counter.MemberCount + 1,
                    },
                };

                member.Value = new Member.Value_T
                {
                    CreateTime = DateTime.UtcNow,
                    UpdateTime = DateTime.UtcNow,
                    //CreateMemberId = memberId,
                    //UpdateMemberId = memberId,

                    Password = input.Password,

                    AccountPK_List = new List<Account.PK_T>
                    {
                        account.PK,
                    },
                    //ScriptureShowList = new List<int> { 1,2,3,4, }, //至少要一個
                    SubjectCollectedList = new List<int> { 1, 2, 3, 4, },
                    ExerciseRecordCreateTimeId_List = new List<string>(),

                    //從第三方取得的資料預填
                    Name = name,
                };
                _cacheRepo.Set(member.GetRedisKeyString(), member.Value);

                counter.MemberCount++;
                _cacheRepo.Set(nameof(EntityCounter), counter);

                account.Value.FK_Member = member.PK;
                _cacheRepo.Set(account.GetRedisKeyString(), account.Value);


                result.Payload = new AccountAndMember
                {
                    Account = account,
                    Member = member,
                };
            }
            catch (Exception ex)
            {
                throw ex;
                //result.FailMessage = ex.Message;//.ToString
            }

            return result;
        }
        public UpdateMember_Output UpdateMember(Action<Member> action)
        {
            var result = new UpdateMember_Output();
            
            var member = GetCurrentMember();
            action(member);
            bool isSuccess = _cacheRepo.Update(member.GetRedisKeyString() , member.Value);

            if (!isSuccess)
            {
                result.FailMessage = "異常：查不到此會員";
                return result;
            }

            return result;
        }

        public UpdateAccount_Output UpdateAccount(string account)
        {
            var result = new UpdateAccount_Output();
            try
            {
                var newEntity = new Account
                {
                    PK = new Account.PK_T
                    {
                        Provider = "Cookie",
                        AccountId_FromProvider = account,
                    },
                };
                //試著查
                newEntity.Value = _cacheRepo.Get<Account.Value_T>(newEntity.GetRedisKeyString());
                if (newEntity.Value != null)
                {
                    result.FailMessage = "此帳號已有人使用";
                    return result;
                }

                //確定 可修改後
                var member = GetCurrentMember();

                newEntity.Value = new Account.Value_T
                {
                    FK_Member = new Member.PK_T
                    {
                        MemberId = member.PK.MemberId,
                    }
                };

                //除舊 佈新
                var oldEntity = new Account
                {
                    PK = member.Value.AccountPK_List[0],
                };
                _cacheRepo.Remove(oldEntity.GetRedisKeyString());
                _cacheRepo.Set(newEntity.GetRedisKeyString(), newEntity.Value);

                member.Value.AccountPK_List[0] = newEntity.PK;
                _cacheRepo.Set(member.GetRedisKeyString(), member.Value);
            }
            catch (Exception ex)
            {
                result.FailMessage = "更新會員資料失敗：" + ex.Message;
            }
            return result;
        }


        public BaseOutput_withPayload<Member> GetMember_ByInput(CreateMember_Input input)
        {
            var result = new BaseOutput_withPayload<Member>();

            var account = new Account
            {
                PK = new Account.PK_T
                {
                    Provider = "Cookie",
                    AccountId_FromProvider = input.Account,
                }
            };

            account.Value = _cacheRepo.Get<Account.Value_T>(account.GetRedisKeyString());

            if (account.Value == null)
            {
                result.FailMessage = "輸入的帳號不存在";
                return result;
            }

            var member = new Member
            {
                PK = account.Value.FK_Member,
            };

            member.Value = _cacheRepo.Get<Member.Value_T>(member.GetRedisKeyString());

            if (member.Value.Password != input.Password)
            {
                result.FailMessage = "密碼錯誤";
                return result;
            }

            result.Payload = member;
            return result;
        }

        public int GetCurrentMemberId()
        {
            return int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
        }

        public Member GetCurrentMember()
        {
            return GetMember_ById( GetCurrentMemberId() );
        }
        public Member GetMember_ById(int memberId)
        {
            var member = new Member
            {
                PK = new Member.PK_T
                {
                    MemberId = memberId,
                },
            };

            member.Value = _cacheRepo.Get<Member.Value_T>(member.GetRedisKeyString());
            
            return member; //Value可能會null
        }
    }
}
