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
            public string Password { get; set; }

            public int ChoicesQuestion_Correct { get; set; }
            public int EssayQuestion_Correct { get; set; }
            public int BlankFillQuestion_Correct { get; set; }
            public int ChoicesQuestion_Done { get; set; }
            public int EssayQuestion_Done { get; set; }
            public int BlankFillQuestion_Done { get; set; }

            //public List<int> ScriptureShowList { get; set; }

            /// <summary>
            /// 每個成員是 SubjectCode ex. A1 、F2
            /// </summary>
            public List<string> SubjectCollectedList { get; set; }

            public List<Account.PK_T> AccountPK_List { get; set; }
            public List<string> ExerciseRecordCreateTimeId_List { get; set; }
        }

        public override string GetRedisKeyString()
        {
            return $"{nameof(Member)}.{PK.MemberId}";
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
