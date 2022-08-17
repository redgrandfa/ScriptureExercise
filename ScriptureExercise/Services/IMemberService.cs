using Common.DBModels;
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
        Member.PK_T GetMemberPK_ByBindKey(string bindKey);
    }
    public class MemberService : BaseService, IMemberService
    {
        public MemberService(
            IHttpContextAccessor httpContextAccessor,
            IMemoryCacheRepository cacheRepo)
            : base(httpContextAccessor, cacheRepo)
        {}
        //private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly IMemoryCacheRepository _cacheRepo;
        //public MemberService(IHttpContextAccessor httpContextAccessor, IMemoryCacheRepository cacheRepo)
        //{
        //    _httpContextAccessor = httpContextAccessor;
        //    _cacheRepo = cacheRepo;
        //}


        public CreateMember_Output CreateMember(CreateMember_Input input)
        {
            var result = new CreateMember_Output();


            var memberPK_Found = GetMemberPK_ByBindKey(input.BindKey);
            //檢查 已存在衝突
            if (memberPK_Found != null)
            {
                result.ErrMsg = "此會員密鑰已被其他人佔用，請換一個";
                return result;
            }

            //創建會員

            var name = _httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            var counter = _cacheRepo.Get<EntityCounter>(nameof(EntityCounter));

            try
            {
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

                    //從第三方取得的資料預填
                    Name = name,

                    FK_BindKey = new BindKey2Member.PK_T
                    {
                        BindKey = input.BindKey,
                    },
                    AccountPK_List = new List<Account.PK_T>(),
                    //{
                    //    new Account.PK_T
                    //    {
                    //        Provider = idClaim.Issuer,
                    //        AccountId_FromProvider = idClaim.Value,
                    //    },
                    //}
                };
                _cacheRepo.Set(member.GetRedisKeyString(), member.Value);

                counter.MemberCount++;
                _cacheRepo.Set(nameof(EntityCounter), counter);

                BindKey2Member bindKey2Member = new BindKey2Member
                {
                    PK = member.Value.FK_BindKey,
                    Value = new BindKey2Member.Value_T
                    {
                        FK_Member = member.PK,
                    },
                };
                _cacheRepo.Set(bindKey2Member.GetRedisKeyString(), bindKey2Member.Value);



                result.OperationResult = true;
                result.MemberCreated = member;
            }
            catch (Exception ex)
            {
                result.ErrMsg = ex.Message;
            }

            return result;
        }


        public Member.PK_T GetMemberPK_ByBindKey(string bindKey)
        {
            var bindKey2Member = new BindKey2Member
            {
                PK = new BindKey2Member.PK_T
                {
                    BindKey = bindKey,
                },
            };

            bindKey2Member.Value = _cacheRepo.Get<BindKey2Member.Value_T>(bindKey2Member.GetRedisKeyString());

            //查不到就null
            return bindKey2Member.Value?.FK_Member;
        }

    }
}
