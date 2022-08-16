using Common.DBModels;

namespace Common.DTOModels.MemberDTOs
{
    public class CreateMember_Input
    {
        public string BindKey { get; set; }
    }
    public class CreateMember_Output : BaseOutput<bool>
    {
        public Member MemberCreated { get; set; }
    }


    //public enum CreateMember__Result
    //{
    //    Success,

    //    [EnumMessage("帳號已存在")]
    //    Error_Exist,
    //}
}
