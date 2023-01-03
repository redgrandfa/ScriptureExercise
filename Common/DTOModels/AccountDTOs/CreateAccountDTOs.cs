using Common.DBModels;

namespace Common.DTOModels.AccountDTOs
{
    public class CreateAccount_Input
    {
        //public string Provider { get; set; }
        //public string Id { get; set; }
        public Member.PK_T MemberPK { get; set; }
    }
    public class CreateAccount_Output : BaseOutput_withPayload<Account>
    {
    }
}
