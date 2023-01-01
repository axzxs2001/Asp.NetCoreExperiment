
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

Test01();
//Test02();
//Test03();

static void Test03()
{
    var wechatCustomer = new WechatChildCustomer
    {
        Name = "张三",
        City = "东京",
        Region = "中央区",
        Address = "1-56-326",
        PostalCode = "3000235",
        EMail = "abcde@gmail.com",
        Tel = "08-9563-2356",
        WechatNo = "wechat_gsw",
        SubWechatNo = "subWechat_gsw",
    };
    var json = JsonSerializer.Serialize<Customer>(wechatCustomer,
        new JsonSerializerOptions
        {
            TypeInfoResolver = new PolymorphicTypeResolver()
        });
    Console.WriteLine(json);
}

static void Test02()
{
    var customers = new List<Customer>();
    var wechatCustomer = new WechatChildCustomer
    {
        Name = "张三",
        City = "东京",
        Region = "中央区",
        Address = "1-56-326",
        PostalCode = "3000235",
        EMail = "abcde@gmail.com",
        Tel = "08-9563-2356",
        WechatNo = "wechat_gsw",
        SubWechatNo = "subWechat_gsw",
    };
    customers.Add(wechatCustomer);
    var lineCustomer = new LineCustomer
    {
        Name = "张三",
        City = "东京",
        Region = "中央区",
        Address = "1-56-326",
        PostalCode = "3000235",
        EMail = "abcde@gmail.com",
        Tel = "08-9563-2356",
        LineNo = "line_gsw"
    };
    customers.Add(lineCustomer);

    var json = JsonSerializer.Serialize<List<Customer>>(customers);
    Console.WriteLine(json);
}

static void Test01()
{
    var wechatCustomer = new WechatCustomer
    {
        Name = "张三",
        City = "东京",
        Region = "中央区",
        Address = "1-56-326",
        PostalCode = "3000235",
        EMail = "abcde@gmail.com",
        Tel = "08-9563-2356",
        WechatNo = "wechat_gsw"
    };
    var lineCustomer = new LineCustomer
    {
        Name = "张三",
        City = "东京",
        Region = "中央区",
        Address = "1-56-326",
        PostalCode = "3000235",
        EMail = "abcde@gmail.com",
        Tel = "08-9563-2356",
        LineNo = "line_gsw"
    };

    //共用打印方法
    void PrintCustomer(Customer customer)
    {
        var options = new JsonSerializerOptions
        {
            TypeInfoResolver = new PolymorphicTypeResolver(),
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };
        var json = JsonSerializer.Serialize<Customer>(customer, options);
        Console.WriteLine(json);
        if (customer is WechatCustomer)
        {
            var wechat = JsonSerializer.Deserialize<WechatCustomer>(json, options);
            Console.WriteLine(wechat);
        }
    }
    void PrintCustomer1(Customer customer)
    {
        var json = JsonSerializer.Serialize<Customer>(customer, new JsonSerializerOptions
        {
            TypeInfoResolver = new PolymorphicTypeResolver()
        });
        Console.WriteLine(json);
    }
    PrintCustomer(wechatCustomer);
    PrintCustomer(lineCustomer);
}
//[JsonPolymorphic(TypeDiscriminatorPropertyName = "customerType")]
////[JsonPolymorphic(TypeDiscriminatorPropertyName = "customerType", UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor)]
////[JsonDerivedType(typeof(Customer), typeDiscriminator: "customer")]
//[JsonDerivedType(typeof(WechatCustomer), typeDiscriminator: "wechatCustomer")]
//[JsonDerivedType(typeof(LineCustomer), typeDiscriminator: "lineCustomer")]
public record Customer
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
    public string PostalCode { get; set; }
    public string EMail { get; set; }
    public string Tel { get; set; }
}

public record WechatCustomer : Customer
{
    public string? WechatNo { get; set; }
}
public record LineCustomer : Customer
{
    public string? LineNo { get; set; }
}

public record WechatChildCustomer : WechatCustomer
{
    public string? SubWechatNo { get; set; }
}



public class PolymorphicTypeResolver : DefaultJsonTypeInfoResolver
{
    public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
    {
        JsonTypeInfo jsonTypeInfo = base.GetTypeInfo(type, options);

        Type basePointType = typeof(Customer);
        if (jsonTypeInfo.Type == basePointType)
        {
            jsonTypeInfo.PolymorphismOptions = new JsonPolymorphismOptions
            {
                TypeDiscriminatorPropertyName = "CustomerType",
                //IgnoreUnrecognizedTypeDiscriminators = true,
                //UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor,
                DerivedTypes =
                {
                    new JsonDerivedType(typeof(WechatCustomer), "wechatCustomer"),
                    new JsonDerivedType(typeof(LineCustomer), "lineCustomer")
                }
            };
        }
        return jsonTypeInfo;
    }
}