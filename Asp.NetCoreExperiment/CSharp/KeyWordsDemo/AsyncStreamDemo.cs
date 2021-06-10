using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Security.Cryptography;
using System.IO;
using Dapper;

namespace KeyWordsDemo
{
    class AsyncStreamDemo : IDemoAsync
    {
        public async Task RunAsync()
        {
            var s = Console.ReadLine();
            var producer = new Producer();
            //var numbers = await producer.GetNumberAsync();
            //foreach (var number in numbers)
            //{
            //    Console.WriteLine(number);
            //}

            //return;

            //await foreach (var number in producer.EnumerateNumbersAsync())
            //{
            //    Console.WriteLine(number);
            //}
            if (s == "1")
            {
                var watch = new Stopwatch();
                watch.Start();
                
                await foreach (var order in producer.EnumerateOrderAsync())
                {
                   Console.WriteLine(order.ToString());
                }
                watch.Stop();
                Console.Title = $"时长：{ watch.Elapsed.TotalSeconds}";

            }
            else
            {
                var watch = new Stopwatch();
                watch.Start();
                var orders = await producer.GetOrdersAsync();            
                foreach (var order in orders)
                {
                    Console.WriteLine(order.ToString());
                }
                watch.Stop();
                Console.Title = $"获取到数据：{orders.Count()} 时长：{ watch.Elapsed.TotalSeconds}";
            }
            Console.ReadLine();
        }
    }
    class Producer
    {
        public async Task<IEnumerable<int>> GetNumberAsync()
        {
            List<int> numbers = new();
            for (var tens = 0; tens < 10; tens++)
            {
                Console.WriteLine("Get some numbers");
                for (var digit = 0; digit < 10; digit++)
                {
                    numbers.Add(tens * 10 + digit);
                }
                Console.Write("Making next request...");
                await Task.Delay(2000);
            }
            return numbers;
        }

        public async IAsyncEnumerable<int> EnumerateNumbersAsync()
        {
            for (var tens = 0; tens < 10; tens++)
            {
                Console.WriteLine("Get some numbers");
                for (var digit = 0; digit < 10; digit++)
                {
                    yield return tens * 10 + digit;
                }
                Console.Write("Making next request...");
                await Task.Delay(2000);
            }

        }

        public async Task<List<SalesOrderDetail>> GetOrdersAsync()
        {
            var orders = new List<SalesOrderDetail>();
            var offset = 0;
            while (true)
            {
                var list = (await QueryOrdersAsync(offset)).ToList();
                orders.AddRange(list);
                offset++;
                if (list.Count < 100)
                {
                    break;
                }
            }
            return orders;
        }

        public async IAsyncEnumerable<SalesOrderDetail> EnumerateOrderAsync()
        {
            var offset = 0;
            while (true)
            {
                var list = (await QueryOrdersAsync(offset)).ToList();
                foreach (var item in list)
                {
                    yield return item;
                }
                offset++;
                if (list.Count < 100)
                {
                    break;
                }
            }

        }
        public async Task<IEnumerable<SalesOrderDetail>> QueryOrdersAsync(int offset)
        {
            using var con = new SqlConnection("server=.;database=AdventureWorks2016;uid=sa;pwd=sa;");
            var sql = @$"select * from Sales.SalesOrderDetail order by SalesOrderID,SalesOrderDetailID  offset {offset*100} row fetch next 100 row only";
            //假如这里醒询有延时
           // await Task.Delay(100);
            return await con.QueryAsync<SalesOrderDetail>(sql);
        }
    }
    class SalesOrderDetail
    {
        public int SalesOrderID { get; set; }
        public string CarrierTrackingNumber { get; set; }
        public short OrderQty { get; set; }
        public int ProductID { get; set; }
        public int SpecialOfferID { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitPriceDiscount { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public override string ToString()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);

        }

    }

    public class DESHelper
    {
        //密钥
        public static byte[] _KEY = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 };
        //向量
        public static byte[] _IV = new byte[] { 0x08, 0x07, 0x06, 0x05, 0x04, 0x03, 0x02, 0x01 };

        /// <summary>
        /// DES加密操作
        /// </summary>
        /// <param name="normalTxt"></param>
        /// <returns></returns>
        public string DesEncrypt(string normalTxt)
        {
            //byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(_KEY);
            //byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(_IV);

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            int i = cryptoProvider.KeySize;
            MemoryStream ms = new MemoryStream();
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(_KEY, _IV), CryptoStreamMode.Write);

            StreamWriter sw = new StreamWriter(cst);
            sw.Write(normalTxt);
            sw.Flush();
            cst.FlushFinalBlock();
            sw.Flush();

            string strRet = Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
            return strRet;
        }

        /// <summary>
        /// DES解密操作
        /// </summary>
        /// <param name="securityTxt">加密字符串</param>
        /// <returns></returns>
        public string DesDecrypt(string securityTxt)//解密  
        {
            //byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(_KEY);
            //byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(_IV);
            byte[] byEnc;
            try
            {
                securityTxt.Replace("_%_", "/");
                securityTxt.Replace("-%-", "#");
                byEnc = Convert.FromBase64String(securityTxt);
            }
            catch
            {
                return null;
            }
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream(byEnc);
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(_KEY, _IV), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cst);
            return sr.ReadToEnd();
        }
    }

}
