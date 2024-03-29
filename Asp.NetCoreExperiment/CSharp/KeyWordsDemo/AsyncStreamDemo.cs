﻿using System;
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

                await foreach (var orders in producer.EnumerateOrdersAsync())
                {
                    foreach (var order in orders)
                    {
                        Console.WriteLine(order.ToString());
                    }
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

        public async IAsyncEnumerable<List<SalesOrderDetail>> EnumerateOrdersAsync()
        {
            var offset = 0;
            while (true)
            {
                var list = (await QueryOrdersAsync(offset)).ToList();
                // foreach (var item in list)
                //{
                yield return list;
                // }
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
            var sql = @$"select * from Sales.SalesOrderDetail order by SalesOrderID,SalesOrderDetailID  offset {offset * 100} row fetch next 100 row only";
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
}
