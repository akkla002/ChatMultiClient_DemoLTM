using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    public class Logger
    {
        public static void Write(string str)
        {
            Console.WriteLine(str);
        }
        public static void ShowByteArr(byte[] arr)
        {
            Console.WriteLine(ByteArrayToString(arr));
        }
        public static void ShowByteArr(byte[] arr,int length)
        {
            Console.WriteLine();
            char[] temp = ByteArrayToString(arr).ToCharArray();
            for (int i = 0; i < length; i++)
            {
                Console.Write(temp[i]);
            }
            Console.WriteLine();
        }
        private static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }
}
