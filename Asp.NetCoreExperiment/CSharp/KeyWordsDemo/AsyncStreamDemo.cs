using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

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
                var sb1 = new StringBuilder();
                await foreach (var order in producer.EnumerateOrderAsync())
                {
                    sb1.AppendLine(order.ToString());
                }
                watch.Stop();
                Console.Title = $"时长：{ watch.Elapsed.TotalSeconds}";

            }
            else
            {
                var watch = new Stopwatch();
                watch.Start();
                var orders = await producer.GetOrdersAsync();
                var sb2 = new StringBuilder();
                foreach (var order in orders)
                {
                    sb2.AppendLine(order.ToString());
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

        public async Task<IEnumerable<POrder>> GetOrdersAsync()
        {
            using var con = new SqlConnection("server=.;database=AdventureWorks2016;uid=sa;pwd=sa;");
            var sql = @"select p.Name,d.UnitPrice,d.OrderQty,a.City,* from Sales.SalesOrderDetail d join Sales.SalesOrderHeader h
on d.SalesOrderID=h.SalesOrderID
join Production.Product p on d.ProductID=p.ProductID
join Person.Address a on a.AddressID=h.ShipToAddressID
left join Sales.SalesPerson pe on pe.BusinessEntityID=h.SalesPersonID
left join sales.SalesOrderHeaderSalesReason r on h.SalesOrderID=r.SalesOrderID";
            var cmd = new SqlCommand(sql, con);
            await con.OpenAsync();
            var reader = await cmd.ExecuteReaderAsync();
            var orders = new List<POrder>();
            while (reader.Read())
            {
                var order = new POrder
                {
                    Name = await reader.GetFieldValueAsync<string>(0),
                    UnitPrice = await reader.GetFieldValueAsync<decimal>(1),
                    OrderQty = await reader.GetFieldValueAsync<short>(2),
                    City = await reader.GetFieldValueAsync<string>(3),
                };
                orders.Add(order);
            }
            return orders;
        }
        public async IAsyncEnumerable<POrder> EnumerateOrderAsync()
        {
            using var con = new SqlConnection("server=.;database=AdventureWorks2016;uid=sa;pwd=sa;");
            var sql = @"select p.Name,d.UnitPrice,d.OrderQty,a.City,* from Sales.SalesOrderDetail d join Sales.SalesOrderHeader h
on d.SalesOrderID=h.SalesOrderID
join Production.Product p on d.ProductID=p.ProductID
join Person.Address a on a.AddressID=h.ShipToAddressID
left join Sales.SalesPerson pe on pe.BusinessEntityID=h.SalesPersonID
left join sales.SalesOrderHeaderSalesReason r on h.SalesOrderID=r.SalesOrderID";
            var cmd = new SqlCommand(sql, con);
            await con.OpenAsync();
            var reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                var order = new POrder
                {
                    Name = await reader.GetFieldValueAsync<string>(0),
                    UnitPrice = await reader.GetFieldValueAsync<decimal>(1),
                    OrderQty = await reader.GetFieldValueAsync<short>(2),
                    City = await reader.GetFieldValueAsync<string>(3),
                };
                yield return order;
            }
        }
    }
    class POrder
    {
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }

        public short OrderQty { get; set; }
        public string City { get; set; }
        public override string ToString()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);

        }

    }
}
