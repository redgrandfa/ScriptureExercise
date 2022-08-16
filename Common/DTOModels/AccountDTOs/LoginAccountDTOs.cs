using Common.DBModels;

namespace Common.DTOModels.AccountDTOs
{
    public class GetAccountInput
    {
        //public string Provider { get; set; }
        //public string Id { get; set; }
        //public string Name { get; set; }
    }
    public class GetAccountOutput : BaseOutput<Account>
    {
    }

    //public enum Login3rdParty_Result
    //{
    //    NewAccount,
    //    LoginSuccess,
    //}


    /// <summary>
    /// 無法在會員中心編修的資訊 = 固定 = 記錄在claim
    /// </summary>
    public class IssueClaimsInput
    {
        public Account Account { get; set; }
        //public Member Member { get; set; }
    }
}
