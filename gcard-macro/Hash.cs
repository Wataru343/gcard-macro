using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyGenerator
{
    public class Hash
    {
        static public string GenerateHash(string text)
        {
            string str = text + "11598753215gandam";
            Console.WriteLine("");

            byte[] data = System.Text.Encoding.UTF8.GetBytes(str);
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();

            byte[] bs = md5.ComputeHash(data);
            md5.Clear();

            string result = BitConverter.ToString(bs).ToUpper().Replace("-", "");

            return result;
        }
    }
}
