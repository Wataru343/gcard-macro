using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ユーザー名を入力してください。");
            string str = System.Console.ReadLine();
            string result = Hash.GenerateHash(str);

            Console.WriteLine("キーをコピーして保管してください。");
            Console.WriteLine(result);
            System.Console.ReadLine();
        }
    }
}
