using ScriptureExercise.Models.AccountVM;

namespace ScriptureExercise.Models.MemberVM
{
    public class MemberEditViewMdel
    {
        //不可改資訊
        public int NumberOfQuestionsDone { get; set; }
        public int NumberOfQuestionsCorrect { get; set; }

        
        /// <summary>
        /// 可自行更改的
        /// </summary>
        MemberUpdateRequestModel Editable { get; set; }
    }

    public class MemberUpdateRequestModel
    {
        public string Name { get; set; }

        public BindMemberFormModel BindMember  {get;set;}  //共用模型檢核...
        //public string BindKey { get; set; }

        //偏好= 經典的隱藏、排序
    }
}
