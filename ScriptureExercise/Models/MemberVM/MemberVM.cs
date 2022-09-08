using ScriptureExercise.Models.AccountVM;
using System;
using System.Collections.Generic;

namespace ScriptureExercise.Models.MemberVM
{
    public class MemberCenterVM
    {
        //不可改資訊
        public string LoginThroughIcon { get; set; }
        public int ChoicesQuestion_Correct { get; set; }
        public int EssayQuestion_Correct { get; set; }
        public int BlankFillQuestion_Correct { get; set; }
        public int ChoicesQuestion_Done { get; set; }
        public int EssayQuestion_Done { get; set; }
        public int BlankFillQuestion_Done { get; set; }

        /// <summary>
        /// 可自行更改的
        /// </summary>
        public MemberEditVM Editable { get; set; }
    }

    public class MemberEditVM
    {
        public string Name { get; set; }
        public string Account { get; set; }
        //public string Password { get; set; }


        //偏好= 經典的隱藏、排序
        public List<int> ScriptureShowList { get; set; }
    }
}
