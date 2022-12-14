namespace ScriptureExercise.Models
{
    public class ApiOperationResult<T> //where T : class
    {
        public Status Status { get; set; }
        public int StatusCode => (int)Status;

        //好像沒必要轉成字串?
        //public string Code => Convert.ToInt32(Response).ToString().PadLeft(3, '0');

        //public T Body { get; set; }
        public T Payload { get; set; }
        public string Message { get; set; }
        //public string Description { get; set; }
    }
    //網路斷線 找不到等等...
    //有溝通到就200

    public enum Status
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0,

        /// <summary>
        /// 未授權
        /// </summary>
        UnAuthorized = 1,

        /// <summary>
        /// 是舊客戶
        /// </summary>
        IsOldMember = 2,

        /// <summary>
        /// 帳號訊息
        /// </summary>
        LoginFail = 101,

        /// <summary>
        /// 新密碼不相符
        /// </summary>
        NewPasswordIsNotMatch = 102,

        /// <summary>
        /// 舊密碼不相符
        /// </summary>
        OldPasswordIsNotMatch = 103,

        /// <summary>
        /// 會員狀態已關閉
        /// </summary>
        MemberIsNotEnable = 104,

        /// <summary>
        /// 找不到資料
        /// </summary>
        FoundNotData = 401,

        /// <summary>
        /// 資料已存在
        /// </summary>
        DataRequireUnique = 402,

        /// <summary>
        /// 缺少必要資料
        /// </summary>
        MissingRequiredInformation = 403,

        /// <summary>
        /// 付款方式不正確
        /// </summary>
        PayMethodNotMatch = 501,

        /// <summary>
        /// 例外錯誤
        /// </summary>
        Exception = 999,
    }

}
