using System;
using System.Data.SqlClient;
using System.Text;
using Dapper;

namespace DapperTimeStampDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var con = new SqlConnection("server=.;database=testdb;uid=sa;pwd=sa;"))
            {
                var max1 = "00-00-00-00-00-00-07-DA";
                //字符串转字节数组
                var bytes = StrToToHexByte(max1);
                var list = con.Query<Test01>("select * from test01 where ts>@ts", new { ts = bytes });
                var max = "0000000000000000";
                foreach (var item in list)
                {
                    //字节数组转字符串
                    var value = BitConverter.ToString(item.TS, 0).Replace("-", string.Empty);
                    if (string.Compare(max, value) < 0)
                    {
                        max = value;
                    }
                }
            }
        }
        private static byte[] StrToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "").Replace("-","");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
    }

    class Test01
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public byte[] TS { get; set; }
    }
}
