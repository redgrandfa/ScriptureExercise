
using System;
namespace Common.DTOModels
{
    public class BaseOutput<T> //where T : Enum 或bool
    {
        //應利用 attribute 製造 enum各項的資訊
        public T OperationResult { get; set; }
        public string ErrMsg { get; set; }
    }
}
