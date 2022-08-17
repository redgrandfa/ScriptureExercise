using ScriptureExercise.Models.AccountVM;
using System;
using System.Collections.Generic;

namespace ScriptureExercise.Models.MemberVM
{
    public class MemberEditViewMdel
    {
        //不可改資訊
        public string LoginThroughIcon { get; set; }
        public int NumberOfQuestionsDone { get; set; }
        public int NumberOfQuestionsCorrect { get; set; }


        /// <summary>
        /// 可自行更改的
        /// </summary>
        public MemberUpdateRequestModel Editable { get; set; }
    }

    public class MemberUpdateRequestModel
    {
        public string Name { get; set; }
        public bool AutoDownload { get; set; }
        public List<int> ScriptueShowList { get; set; }

        public CreateMemberFormModel BindMember  {get;set;}  //創建時就有的欄位，共用模型檢核...
        //偏好= 經典的隱藏、排序
    }
}
