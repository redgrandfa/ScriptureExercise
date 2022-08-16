using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DBModels
{
    //只有一筆，紀錄會員總數量，以發識別規格的memberId
    //'memberCount':1
    public class Member : RedisEntity< Member.PK_T, Member.Value_T >
    {
        public class PK_T
        {
            public int MemberId { get; set; }
        }

        public class Value_T : CommonFields
        {
            public string Name { get; set; }
            public int NumberOfQuestionDone { get; set; }
            public int NumberOfQuestionCorrect { get; set; }

            public BindKey2Member.PK_T FK_BindKey { get; set; }
            public List<Account.PK_T> AccountPK_List { get; set; }

        }

        public override string GetRedisKeyString()
        {
            return $"{nameof(Member)}.{PK.MemberId}";
        }
    }



    public class BindKey2Member : RedisEntity<BindKey2Member.PK_T, BindKey2Member.Value_T>
    {
        public class PK_T
        {
            public string BindKey { get; set; }
        }
        public class Value_T
        {
            public Member.PK_T FK_Member { get; set; }
        }
        public override string GetRedisKeyString()
        {
            return $"{nameof(BindKey2Member)}.{PK.BindKey}";
        }
    }
    public class Account : RedisEntity<  Account.PK_T, Account.Value_T  >
    {
        public class PK_T
        {
            public string Provider { get; set; }
            public string AccountId_FromProvider { get; set; }
        }

        public class Value_T : CommonFields
        {
            public Member.PK_T FK_Member { get; set; }
        }

        public override string GetRedisKeyString()
        {
            return $"{nameof(Account)}.{PK.Provider}_{PK.AccountId_FromProvider}";
        }
    }
}
