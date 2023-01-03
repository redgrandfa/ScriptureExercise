using Common.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOModels.ExcerciseDTOs
{
    public class CreateExerciseRecordInput
    {
        public DateTime CreateTime { get; set; }
        public string ExerciseJsonFileName { get; set; }
        //public string PaperName { get; set; }
        public string ReplyJSON { get; set; }
        public int PercentScore { get; set; }
        public int Score { get; set; }
    }

    public class CreateExerciseRecordOutput: BaseOutput_withPayload<string>
    {

    }
}
