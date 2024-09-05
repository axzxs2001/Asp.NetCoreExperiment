using System.Text.Json;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.ComponentModel;
using System.Globalization;

var chatModelId = "gpt-4o";
var key = File.ReadAllText(@"C:\GPT\key.txt");

var kernel = Kernel.CreateBuilder()
   .AddOpenAIChatCompletion(chatModelId, key)
   .Build();

//var promptFunctionDescribe = "请给出{{$parameter}}的前五大城市的人口数量和经济规模";
//var promptFunction = kernel.CreateFunctionFromPrompt(promptFunctionDescribe);
//var result = await kernel.InvokeAsync(promptFunction, new() { ["parameter"] = "日本" });
//Console.WriteLine(result.GetValue<string>());
//Console.WriteLine(JsonSerializer.Serialize(result.Metadata?["Usage"]));

//var timer = new Timer();
//var nativeFunction = kernel.CreateFunctionFromMethod(typeof(Timer).GetMethod("GetCurrentTime"), target: timer);
//var result = await nativeFunction.InvokeAsync(kernel);
//var time = result.GetValue<DateTime>();
//Console.WriteLine("当前UTC时间：{0}", time);


kernel.ImportPluginFromType<DataBase>();
var salesAnalysisFunctions = kernel.ImportPluginFromType<SalesAnalysis>();
var result = await kernel.InvokeAsync<string>(salesAnalysisFunctions["Analysis"]);
Console.WriteLine(result);


class Timer
{
    [KernelFunction]
    public DateTime GetCurrentTime()
    {
        return DateTime.UtcNow;
    }
}

class SalesAnalysis
{
    [KernelFunction]
    public async Task<string?> AnalysisAsync(Kernel kernel)
    {
        var data = await kernel.InvokeAsync<string>(nameof(DataBase), "GetSalesData", arguments: new() { ["month"] = "July" });
        Console.WriteLine("查询数据如下：\r\n{0}", data);
        Console.WriteLine("分析中……");
        var result = await kernel.InvokePromptAsync("请根据下面的json数据，综合每个人的销售总额和件数，以及它们的关系，给出销售能力评价。要求：不需要给出分析过程，只用以控制台表格形式给出分析结果，评价用中文书写。数据：{{$data}}", new() { ["data"] = data });
        return result.GetValue<string>();
    }
}
public class DataBase
{
    List<SalesRecord> salesData = new List<SalesRecord>
        {
            // Alice's sales data
            new SalesRecord("Alice", "January", 50, 1000.50m),
            new SalesRecord("Alice", "February", 45, 950.75m),
            new SalesRecord("Alice", "March", 60, 1200.30m),
            new SalesRecord("Alice", "April", 55, 1100.40m),
            new SalesRecord("Alice", "May", 70, 1500.80m),
            new SalesRecord("Alice", "June", 65, 1300.00m),
            new SalesRecord("Alice", "July", 75, 1550.25m),
            new SalesRecord("Alice", "August", 80, 1600.00m),
            new SalesRecord("Alice", "September", 85, 1700.75m),
            new SalesRecord("Alice", "October", 90, 1800.90m),
            new SalesRecord("Alice", "November", 95, 1900.60m),
            new SalesRecord("Alice", "December", 100, 2000.00m),

            // Bob's sales data
            new SalesRecord("Bob", "January", 30, 600.00m),
            new SalesRecord("Bob", "February", 35, 700.50m),
            new SalesRecord("Bob", "March", 40, 800.25m),
            new SalesRecord("Bob", "April", 32, 640.75m),
            new SalesRecord("Bob", "May", 38, 760.00m),
            new SalesRecord("Bob", "June", 42, 820.60m),
            new SalesRecord("Bob", "July", 47, 940.90m),
            new SalesRecord("Bob", "August", 50, 1000.00m),
            new SalesRecord("Bob", "September", 55, 1100.30m),
            new SalesRecord("Bob", "October", 60, 1200.50m),
            new SalesRecord("Bob", "November", 65, 1300.75m),
            new SalesRecord("Bob", "December", 70, 1400.00m),

            // Charlie's sales data
            new SalesRecord("Charlie", "January", 25, 500.00m),
            new SalesRecord("Charlie", "February", 28, 560.00m),
            new SalesRecord("Charlie", "March", 35, 700.00m),
            new SalesRecord("Charlie", "April", 30, 600.00m),
            new SalesRecord("Charlie", "May", 40, 800.00m),
            new SalesRecord("Charlie", "June", 45, 900.00m),
            new SalesRecord("Charlie", "July", 50, 5000.00m),
            new SalesRecord("Charlie", "August", 55, 1100.00m),
            new SalesRecord("Charlie", "September", 60, 1200.00m),
            new SalesRecord("Charlie", "October", 65, 1300.00m),
            new SalesRecord("Charlie", "November", 70, 1400.00m),
            new SalesRecord("Charlie", "December", 75, 1500.00m),

            // Diana's sales data
            new SalesRecord("Diana", "January", 40, 800.00m),
            new SalesRecord("Diana", "February", 42, 840.00m),
            new SalesRecord("Diana", "March", 45, 900.00m),
            new SalesRecord("Diana", "April", 50, 1000.00m),
            new SalesRecord("Diana", "May", 55, 1100.00m),
            new SalesRecord("Diana", "June", 60, 1200.00m),
            new SalesRecord("Diana", "July", 65, 1300.00m),
            new SalesRecord("Diana", "August", 70, 1400.00m),
            new SalesRecord("Diana", "September", 75, 1500.00m),
            new SalesRecord("Diana", "October", 80, 1600.00m),
            new SalesRecord("Diana", "November", 85, 1700.00m),
            new SalesRecord("Diana", "December", 90, 1800.00m),

            // Ethan's sales data
            new SalesRecord("Ethan", "January", 15, 300.00m),
            new SalesRecord("Ethan", "February", 18, 360.00m),
            new SalesRecord("Ethan", "March", 20, 400.00m),
            new SalesRecord("Ethan", "April", 25, 500.00m),
            new SalesRecord("Ethan", "May", 30, 600.00m),
            new SalesRecord("Ethan", "June", 35, 700.00m),
            new SalesRecord("Ethan", "July", 40, 800.00m),
            new SalesRecord("Ethan", "August", 45, 900.00m),
            new SalesRecord("Ethan", "September", 50, 1000.00m),
            new SalesRecord("Ethan", "October", 55, 1100.00m),
            new SalesRecord("Ethan", "November", 60, 1200.00m),
            new SalesRecord("Ethan", "December", 65, 1300.00m)
        };
    [KernelFunction]
    public async Task<string> GetSalesDataAsync(string month)
    {
        var salesRecords = salesData.Where(s => s.Month == month);
        if (salesRecords == null)
        {
            return "No sales data found for the specified name and month.";
        }
        return JsonSerializer.Serialize(salesRecords);
    }
    public class SalesRecord
    {
        public string Name { get; set; }
        public string Month { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }

        public SalesRecord(string name, string month, int quantity, decimal amount)
        {
            Name = name;
            Month = month;
            Quantity = quantity;
            Amount = amount;
        }
    }

}