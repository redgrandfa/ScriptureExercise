using Common.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOModels.ExcerciseDTOs
{
    public class GetRecordListOutput: BaseOutput_withPayload< List<RecordRow> >
    {

    }

    public class RecordRow
    {
        public string CreateTime { get; set; }
        public string ExerciseJsonFileName { get; set; }
        //public string PaperName { get; set; }
        public int PercentScore { get; set; }
    }

}
