using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public static class Encryption
    {
        //plaintext     明文 
        //ciphertext    密文
        //encoded       被編碼過的
        //encrypted     被加密的
        public static string SHA256(string plaintext)
        {
            //long => Int64
            //int => Int32
            //short => Int16
            //byte 就像是 Int8   ，一個位元組(byte)  = 8位元(bit)

            byte[] source = plaintext.Select(c => (byte)c).ToArray();

            byte[] enctypted = new SHA256CryptoServiceProvider().ComputeHash(source);

            //2^8  = 16^2 
            string ciphertext = string.Concat(
                enctypted.Select(x => x.ToString("X2"))
            // 格式化字串，X代表十六進位，2代表總共兩位
            );

            return ciphertext.ToUpper(); //看要不要全大寫
        }

        //public static string ToMD5(this string strSource)
        //{
        //    //using var cryptoMD5 = MD5.Create();
        //    using (var cryptoMD5 = MD5.Create())
        //    {
        //        //將字串編碼成 UTF8 位元組陣列
        //        var bytes = Encoding.UTF8.GetBytes(strSource);

        //        //取得雜湊值位元組陣列
        //        var hash = cryptoMD5.ComputeHash(bytes);

        //        //取得 MD5
        //        var md5 = BitConverter.ToString(hash)
        //            .Replace("-", string.Empty)
        //            .ToUpper();

        //        return md5;
        //    }
        //}
    }
}
