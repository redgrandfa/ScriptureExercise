
using System;
namespace Common.DTOModels
{
    public class BaseOutput<T> //where T : Enum 或bool
    {
        //應利用 attribute 製造 enum各項的資訊
        public T OperationResult { get; set; }
        public string ErrMsg { get; set; }
    }


    //讀
    //寫入的 輸出 => 只有成敗 => ErrMsg

    //R => 沒有例外、回傳有null與否
    //C => 有例外， 有可能需要回傳寫入的結果
    //
    //U => 只有成敗 => ErrMsg   有可能需要回傳寫入的結果??
    //D => 只有成敗 => ErrMsg
}
