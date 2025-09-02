using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace A2A;
/// <summary>
/// 一个简单的发票插件，返回模拟数据。
/// </summary>
public class Product
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; } // 单价  

    public Product(string name, int quantity, decimal price)
    {
        this.Name = name;
        this.Quantity = quantity;
        this.Price = price;
    }

    public decimal TotalPrice()
    {
        return this.Quantity * this.Price; // 该产品的总价  
    }
}

public class Invoice
{
    public string TransactionId { get; set; }
    public string InvoiceId { get; set; }
    public string CompanyName { get; set; }
    public DateTime InvoiceDate { get; set; }
    public List<Product> Products { get; set; } // 产品列表  

    public Invoice(string transactionId, string invoiceId, string companyName, DateTime invoiceDate, List<Product> products)
    {
        this.TransactionId = transactionId;
        this.InvoiceId = invoiceId;
        this.CompanyName = companyName;
        this.InvoiceDate = invoiceDate;
        this.Products = products;
    }

    public decimal TotalInvoicePrice()
    {
        return this.Products.Sum(product => product.TotalPrice()); // 发票中所有产品的总价  
    }
}

public class InvoiceQueryPlugin
{
    private readonly List<Invoice> _invoices;
    private static readonly Random s_random = new();

    public InvoiceQueryPlugin()
    {
        // 扩展的模拟数据，包含数量和价格  
        this._invoices =
        [
            new("TICKET-XYZ987", "INV789", "Contoso", GetRandomDateWithinLastTwoMonths(), new List<Product>
            {
                new("T恤", 150, 10.00m),
                new("帽子", 200, 15.00m),
                new("眼镜", 300, 5.00m)
            }),
            new("TICKET-XYZ111", "INV111", "XStore", GetRandomDateWithinLastTwoMonths(), new List<Product>
            {
                new("T恤", 2500, 12.00m),
                new("帽子", 1500, 8.00m),
                new("眼镜", 200, 20.00m)
            }),
            new("TICKET-XYZ222", "INV222",  "Cymbal Direct", GetRandomDateWithinLastTwoMonths(), new List<Product>
            {
                new("T恤", 1200, 14.00m),
                new("帽子", 800, 7.00m),
                new("眼镜", 500, 25.00m)
            }),
            new("TICKET-XYZ333", "INV333", "Contoso", GetRandomDateWithinLastTwoMonths(), new List<Product>
            {
                new("T恤", 400, 11.00m),
                new("帽子", 600, 15.00m),
                new("眼镜", 700, 5.00m)
            }),
            new("TICKET-XYZ444", "INV444", "XStore", GetRandomDateWithinLastTwoMonths(), new List<Product>
            {
                new("T恤", 800, 10.00m),
                new("帽子", 500, 18.00m),
                new("眼镜", 300, 22.00m)
            }),
            new("TICKET-XYZ555", "INV555", "Cymbal Direct", GetRandomDateWithinLastTwoMonths(), new List<Product>
            {
                new("T恤", 1100, 9.00m),
                new("帽子", 900, 12.00m),
                new("眼镜", 1200, 15.00m)
            }),
            new("TICKET-XYZ666", "INV666", "Contoso", GetRandomDateWithinLastTwoMonths(), new List<Product>
            {
                new("T恤", 2500, 8.00m),
                new("帽子", 1200, 10.00m),
                new("眼镜", 1000, 6.00m)
            }),
            new("TICKET-XYZ777", "INV777", "XStore", GetRandomDateWithinLastTwoMonths(), new List<Product>
            {
                new("T恤", 1900, 13.00m),
                new("帽子", 1300, 16.00m),
                new("眼镜", 800, 19.00m)
            }),
            new("TICKET-XYZ888", "INV888", "Cymbal Direct", GetRandomDateWithinLastTwoMonths(), new List<Product>
            {
                new("T恤", 2200, 11.00m),
                new("帽子", 1700, 8.50m),
                new("眼镜", 600, 21.00m)
            }),
            new("TICKET-XYZ999", "INV999", "Contoso", GetRandomDateWithinLastTwoMonths(), new List<Product>
            {
                new("T恤", 1400, 10.50m),
                new("帽子", 1100, 9.00m),
                new("眼镜", 950, 12.00m)
            })
        ];
    }

    public static DateTime GetRandomDateWithinLastTwoMonths()
    {
        // 获取当前日期和时间  
        DateTime endDate = DateTime.Now;

        // 计算开始日期，即当前日期前两个月  
        DateTime startDate = endDate.AddMonths(-2);

        // 生成一个介于0到范围内总天数之间的随机天数  
        int totalDays = (endDate - startDate).Days;
        int randomDays = s_random.Next(0, totalDays + 1); // +1 包含结束日期  

        // 返回随机日期  
        return startDate.AddDays(randomDays);
    }

    [KernelFunction]
    [Description("查询指定公司的发票，可选择在指定时间范围内")]
    public IEnumerable<Invoice> QueryInvoices(string companyName, DateTime? startDate = null, DateTime? endDate = null)
    {
        var query = this._invoices.Where(i => i.CompanyName.Equals(companyName, StringComparison.OrdinalIgnoreCase));

        if (startDate.HasValue)
        {
            query = query.Where(i => i.InvoiceDate >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            query = query.Where(i => i.InvoiceDate <= endDate.Value);
        }

        return query.ToList();
    }

    [KernelFunction]
    [Description("使用交易ID查询发票")]
    public IEnumerable<Invoice> QueryByTransactionId(string transactionId)
    {
        var query = this._invoices.Where(i => i.TransactionId.Equals(transactionId, StringComparison.OrdinalIgnoreCase));

        return query.ToList();
    }

    [KernelFunction]
    [Description("使用发票ID查询发票")]
    public IEnumerable<Invoice> QueryByInvoiceId([Description("发票ID")]string invoiceId)
    {
        var query = this._invoices.Where(i => i.InvoiceId.Equals(invoiceId, StringComparison.OrdinalIgnoreCase));

        return query.ToList();
    }
}
