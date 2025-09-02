using A2A;
using A2A.AspNetCore;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.A2A;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


var arr = File.ReadAllLines("C:/gpt/azure_key.txt");
string? apiKey = arr[2];
string? endpoint = arr[1];
string modelId = arr[0];

IEnumerable<KernelPlugin> invoicePlugins = [KernelPluginFactory.CreateFromType<StoreSystemPlugin>()];

A2AHostAgent? hostAgent =  CreateChatCompletionHostAgent(
            modelId, endpoint, apiKey, "ShopAgent",
            """
            您专门处理与商店进销存的相关的请求。
            """, invoicePlugins);

app.MapA2A(hostAgent!.TaskManager!, "/");

await app.RunAsync();


A2AHostAgent CreateChatCompletionHostAgent(string modelId, string endpoint, string apiKey, string name, string instructions, IEnumerable<KernelPlugin>? plugins = null)
{
    var builder = Kernel.CreateBuilder();
    builder.AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey);
    if (plugins is not null)
    {
        foreach (var plugin in plugins)
        {
            builder.Plugins.Add(plugin);
        }
    }
    var kernel = builder.Build();

    var agent = new ChatCompletionAgent()
    {
        Kernel = kernel,
        Name = name,
        Instructions = instructions,
        Arguments = new KernelArguments(new PromptExecutionSettings() { FunctionChoiceBehavior = FunctionChoiceBehavior.Auto() }),
    };

    var agentCard = GetShopAgentCard();

    return new A2AHostAgent(agent, agentCard);
}

AgentCard GetShopAgentCard()
{
    var capabilities = new AgentCapabilities()
    {
        Streaming = false,
        PushNotifications = false,
    };

    var invoiceQuery = new AgentSkill()
    {
        Id = "id_shop_agent",
        Name = "ShopAgent",
        Description = "处理与商店进销存的相关的请求。",
        Tags = ["发票", "semantic-kernel"],
        Examples =
        [
           "按照水果名称(Name)查询水果",
        ],
    };

    return new()
    {
        Name = "ShopAgent",
        Description = "处理与商店进销存的相关的请求。",
        Version = "1.0.0",
        DefaultInputModes = ["text"],
        DefaultOutputModes = ["text"],
        Capabilities = capabilities,
        Skills = [invoiceQuery],
    };
}

public class StoreSystemPlugin
{
    public List<Goods> GoodsList { get; set; } = new List<Goods>
        {
            new Goods("苹果",5,100),
            new Goods("香蕉",3,200),
            new Goods("橙子",4,150),
            new Goods("桃子",6,120),
            new Goods("梨",5,100),
            new Goods("葡萄",7,80),
            new Goods("西瓜",8,60),
            new Goods("菠萝",9,40),
            new Goods("芒果",10,30),
            new Goods("草莓",11,20),
            new Goods("柠檬",4,100),
            new Goods("橘子",3,100),
            new Goods("蓝莓",6,100),
            new Goods("樱桃",7,100),
            new Goods("葡萄柚",8,100),
            new Goods("柚子",9,100),
            new Goods("榴莲",10,100),
            new Goods("火龙果",11,100),
            new Goods("荔枝",12,100),
            new Goods("椰子",13,100),
            new Goods("桑葚",5,100),
            new Goods("杨梅",4,100),
            new Goods("树梅",6,100),
            new Goods("莓子",7,100),
            new Goods("石榴",8,100),
            new Goods("蜜桃",9,100),
        };
    public decimal Total { get; set; } = 0;
    [KernelFunction]
    [Description("按照水果名称(Name)查询水果")]
    public string GetGoodsByName([Description("水果名称")] string name)
    {
        return GoodsList.FirstOrDefault(g => g.Name == name)?.ToString() ?? "未找到水果";
    }
    [KernelFunction]
    [Description("查询单价(Price)少于等于参数的所有水果")]
    public string GetGoodsLessEqualsPrice([Description("水果单价")] decimal price)
    {
        var goodses = GoodsList.Where(g => g.Price <= price);
        if (goodses == null || goodses.Any() == false)
        {
            return "未找到水果";
        }
        else
        {
            return string.Join("\n", goodses);
        }
    }
    [Description("查询单价(Price)少于参数的所有水果")]
    public string GetGoodsLessPrice([Description("水果单价")] decimal price)
    {
        var goodses = GoodsList.Where(g => g.Price < price);
        if (goodses == null || goodses.Any() == false)
        {
            return "未找到水果";
        }
        else
        {
            return string.Join("\n", goodses);
        }
    }
    [KernelFunction]
    [Description("查询单价(Price)大于等于参数的所有水果")]
    public string GetGoodsGreaterEqualsPrice([Description("水果单价")] decimal price)
    {
        var goodses = GoodsList.Where(g => g.Price >= price);
        if (goodses == null || goodses.Any() == false)
        {
            return "未找到水果";
        }
        else
        {
            return string.Join("\n", goodses);
        }
    }
    [KernelFunction]
    [Description("查询单价(Price)大于参数的所有水果")]
    public string GetGoodsGreaterPrice([Description("水果单价")] decimal price)
    {
        var goodses = GoodsList.Where(g => g.Price > price);
        if (goodses == null || goodses.Any() == false)
        {
            return "未找到水果";
        }
        else
        {
            return string.Join("\n", goodses);
        }
    }

    [KernelFunction]
    [Description("查询库存数量(Quantity)大于等于参数的所有水果")]
    public string GetGoodsGreaterEqualsQuantity([Description("水果库存数量")] int quantity)
    {
        var goodses = GoodsList.Where(g => g.Quantity >= quantity);
        if (goodses == null || goodses.Any() == false)
        {
            return "未找到水果";
        }
        else
        {
            return string.Join("\n", goodses);
        }
    }

    [KernelFunction]
    [Description("查询库存数量(Quantity)大于参数的所有水果")]
    public string GetGoodsGreaterQuantity([Description("水果库存数量")] int quantity)
    {
        var goodses = GoodsList.Where(g => g.Quantity > quantity);
        if (goodses == null || goodses.Any() == false)
        {
            return "未找到水果";
        }
        else
        {
            return string.Join("\n", goodses);
        }
    }
    [KernelFunction]
    [Description("查询库存数量(Quantity)少于等于参数的所有水果")]
    public string GetGoodsLessEqualsQuantity([Description("水果数量")] int quantity)
    {
        var goodses = GoodsList.Where(g => g.Quantity <= quantity);
        if (goodses == null || goodses.Any() == false)
        {
            return "未找到水果";
        }
        else
        {
            return string.Join("\n", goodses);
        }
    }
    [KernelFunction]
    [Description("查询库存数量(Quantity)少于参数的所有水果")]
    public string GetGoodsLessQuantity([Description("水果数量")] int quantity)
    {
        var goodses = GoodsList.Where(g => g.Quantity < quantity);
        if (goodses == null || goodses.Any() == false)
        {
            return "未找到水果";
        }
        else
        {
            return string.Join("\n", goodses);
        }
    }
    [KernelFunction]
    [Description("购买水果")]
    public string BuyGoods([Description("水果名称")] string name, [Description("购买数量")] int quantity)
    {
        var goods = GoodsList.FirstOrDefault(g => g.Name == name);
        if (goods != null)
        {
            var newQuantity = goods.Quantity - quantity;
            if (newQuantity < 0)
            {
                return "库存不足";
            }
            else
            {
                goods.Quantity = newQuantity;
                goods.BuyQuantity += quantity;
                Total += goods.Price * quantity;
                return "购买成功！";
            }
        }
        else
        {
            return "未找到水果";
        }
    }
}
public class Goods
{
    public Goods(string name, decimal price, int quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public int BuyQuantity { get; set; } = 0;

    public override string ToString()
    {
        return $"名称(Name):{Name},单价(Price):{Price},库存数量(Quantity):{Quantity},销售数量(BuyQuantity):{BuyQuantity}";
    }
}