using ModelContextProtocol.Server;
using System.ComponentModel;

namespace MCPOrderTool.Tools;

[McpServerToolType]
public static class OrderTool
{
    [McpServerTool("QueryOrder"), Description("按开始日期和结速日期查询订单")]
    public static List<Order> QueryOrder(DateTime? beginTime, DateTime? endTime)
    {
        Console.WriteLine("-------------参数-------------");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"BeginTime:{beginTime}，EndTime：{endTime}");
        Console.ResetColor();
        Console.WriteLine("-------------参数-------------");
        if (beginTime is null || endTime is null)
        {
            beginTime = DateTime.Now;
            endTime = DateTime.Now;
        }
        return new List<Order>
        {
            new Order("NO000001", "Order1", DateTime.Now.ToString("yyyy-MM-dd"), "Pending"),
            new Order("NO000002", "Order2", DateTime.Now.ToString("yyyy-MM-dd"), "Completed"),
            new Order("NO000003", "Order3", DateTime.Now.ToString("yyyy-MM-dd"), "Pending")           
        };
    }
}

public record Order(string OrderId, string OrderName, string OrderTime, string OrderStatus);