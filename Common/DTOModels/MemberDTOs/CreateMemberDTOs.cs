﻿using Common.DBModels;

namespace Common.DTOModels.MemberDTOs
{
    public class CreateMember_Input
    {
        public string Account { get; set; }
        public string Password { get; set; }
    }
    public class CreateMember_Output : BaseOutput_withPayload<AccountAndMember> //若成功就回傳創好的Member
    {
    }
    public class AccountAndMember{
        public Account Account { get; set; }
        public Member Member { get; set; }
    }


    //public enum CreateMember__Result
    //{
    //    Success,

    //    [EnumMessage("帳號已存在")]
    //    Error_Exist,
    //}
}
