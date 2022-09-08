using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DBModels
{
    public class ExerciseRecord : RedisEntity<ExerciseRecord.PK_T, ExerciseRecord.Value_T>
    {
        public class PK_T
        {
            public Member.PK_T FK_Member { get; set; }
            public string CreateTimeId { get; set; }
        }

        public class Value_T
        {
            public string FK_JsonFileName { get; set; }
            //public string PaperName { get; set; }
            public string ReplyJSON { get; set; }

            public int PercentScore { get; set; }
            public int Score { get; set; }
        }
        public override string GetRedisKeyString()
        {
            return $"{nameof(ExerciseRecord)}.{PK.FK_Member.MemberId}_{PK.CreateTimeId}";
        }
    }

    //public abstract class Reply
    //{
    //    public int Id { get; set; }
    //}

    //public class ChoicesQuestion_Reply : Reply
    //{ 
    //    public int Choosed { get; set; }
    //}
    //public class EssayQuestion_Reply : Reply
    //{
    //    public string Reply { get; set; }
    //}
    //public class BlankFillQuestion_Reply : Reply
    //{
    //    public List<string> Replies { get; set; }
    //}
}
