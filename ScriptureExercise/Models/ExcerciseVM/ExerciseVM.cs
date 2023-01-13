using Common.DBModels;
using Common.DTOModels.ExerciseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScriptureExercise.Models.ExerciseVM
{
    public class PostPaperRequestModel
    {
        public Member MemberUpdate { get; set; }
        //public Record RecordCreate { get; set; }
        public CreateExerciseRecordInput RecordCreate { get; set; }


        public class Member
        {
            public int ChoicesQuestion_Correct { get; set; }
            public int EssayQuestion_Correct { get; set; }
            public int BlankFillQuestion_Correct { get; set; }
            public int ChoicesQuestion_Done { get; set; }
            public int EssayQuestion_Done { get; set; }
            public int BlankFillQuestion_Done { get; set; }
        }

        //public class Record
        //{
        //    public string ExerciseJsonPath { get; set; }
        //    public string ReplyJSON { get; set; }
        //    public int Score { get; set; }
        //    public int PercentScore { get; set; }
        //}
    }

    //public class RecordListVM { 
    //    List<RecordRowVM> RecordList { get; set; }

    //}

    //public class PaperVM
    //{
    //    List<Problem<object>> Problems { get; set; }
    //    public PaperVM(List<Problem<object>> problems)
    //    {
    //        Problems = problems;
    //    }
    //    public abstract class Problem<T>
    //    {
    //        string ProblemStem { get; }
    //        T Answer { get; set; }
    //        T CorrectAnswer { get; }
    //        //獨立配分
    //        double Allocation { get; set; }
    //        protected Problem(string problemStem, T correctAnswer)
    //        {
    //            ProblemStem = problemStem;
    //            CorrectAnswer = correctAnswer;
    //        }

    //        public class SelectProblem : Problem<int>
    //        {
    //            public List<string> Selections { get; }

    //            public SelectProblem(string problemBody, int correctAnswer, List<string> selections) :
    //                base(problemBody, correctAnswer)
    //            {
    //                Selections = selections;
    //            }
    //        }
    //        public class EssayProblem : Problem<string>
    //        {
    //            public EssayProblem(string problemBody, string correctAnswer) :
    //                base(problemBody, correctAnswer)
    //            { }
    //        }
    //        public class BlankFillProblem : Problem<List<string>>
    //        {
    //            public BlankFillProblem(string problemBody, List<string> correctAnswer) :
    //                base(problemBody, correctAnswer)
    //            { }
    //        }

    //    }
    //}
    //class OnGoingExam : PaperVM
    //{
    //    public OnGoingExam(List<Problem<object>> problems) : base(problems)
    //    {
    //    }
    //    DateTime TempTime { get; set; }
    //    TimeSpan CurrentUsedTime { get; set; }
    //    void SubmitPageAndJudge() { }

    //}
    //class FinishedExam : PaperVM
    //{
    //    ExamResult Result { get; }

    //    FinishedExam(List<Problem<object>> problems, ExamResult result) : base(problems)
    //    {
    //        Result = result;
    //    }
    //    public class ExamResult
    //    {
    //        decimal Score { get; }
    //        DateTime SubmitTime { get; }
    //        TimeSpan TotalUsedTime { get; }
    //    }
    //}
}
