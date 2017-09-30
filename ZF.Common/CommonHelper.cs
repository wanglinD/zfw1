using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ZF.Common
{
    public class CommonHelper
    {
        //MD5加密
        public static string CalcMD5(string str)
        {
            //把字符串编码变成byte数组。调用CalcMd4
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            return CalcMD5(bytes);
        }

        /// <summary>
        /// 把bytes类型的数据MD5加密
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string CalcMD5(byte[] bytes)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] computeBytes = md5.ComputeHash(bytes);
                string result = "";
                for (int i = 0;i < computeBytes.Length; i++)
                {
                    //ToString("X") 转换为16进制
                    result += computeBytes[i].ToString("X").Length == 1 ? "0" + computeBytes[i].ToString("X") : computeBytes[i].ToString("X");

                }
                return result;
            }
        }


        public static string CalcMD5(Stream stream )
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] computeBytes = md5.ComputeHash(stream);
                string result = "";
                for(int i = 0;i<computeBytes.Length;i++)
                {
                    result += computeBytes[i].ToString("X").Length == 1 ? "0" + computeBytes[i].ToString("X") : computeBytes[i].ToString("X");

                }
                return result;
            }
        }
        /// <summary>
        /// 验证码生成
        /// </summary>
        /// <param name="len">调用方法时定义验证码长度</param>
        /// <returns></returns>
        //Chapcha
        public static string CreateVerifyCode(int len)
        {
            char[] data = { 'a','c','d','e','f','h','k','m',
                'n','r','s','t','w','x','y'};
            StringBuilder sb = new StringBuilder();
            Random rand = new Random();
            for (int i = 0; i < len; i++)
            {
                //生成0-data.Length的随机数一个
                int index = rand.Next(0,data.Length);//[0,data.length)
                char ch = data[index];//根据每次生成的随机数作为下标取出data中的元素，做为验证码
                sb.Append(ch);
            }
            //勤测！
            return sb.ToString();
        }
    }
}

