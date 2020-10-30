using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArchitectureDemo04
{
    public static class StringExtension
    {  
        /// <summary>
        /// 右边不够长度补空格，汉字算两个空格
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length">设定长度</param>
        /// <returns></returns>
        public static string ChineseCharacterLeft(this string str, int length)
        {
            var len = Encoding.Default.GetBytes(str).Length;
            if (len < length)
            {
                for (int i = 0; i < length - len; i++)
                {
                    str = " " + str;
                }
            }
            return str;
        }

        /// <summary>
        /// 右边不够长度补空格，汉字算两个空格
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length">设定长度</param>
        /// <returns></returns>
        public static string ChineseCharacterRight(this string str, int length)
        {
            var len = Encoding.Default.GetBytes(str).Length;
            if (len < length)
            {
                for (int i = 0; i < length - len; i++)
                {
                    str += " ";
                }
            }
            return str;
        }

        /// <summary>
        /// 切除字符串
        /// </summary>
        public static string ChineseCharacterSubstring(this string str, int length, out string remaining)
        {
            var arr = Encoding.Default.GetBytes(str);
            var barr = arr.Take(length).ToArray();
            var valuestr = Encoding.Default.GetString(barr);
            barr = arr.Skip(length).ToArray();
            remaining = Encoding.Default.GetString(barr); ;
            return valuestr;
        }
    }
}
