using System.Text.Json;

var options = new JsonSerializerOptions
{
    //启用缩进
    WriteIndented = true,
    //缩进字符
    IndentCharacter = ' ',
    //缩进大小
    IndentSize = 6,

    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
};

string json = JsonSerializer.Serialize(
    new { Name = "张三", Age = 20, Sex = "男" },
    options
    );
Console.WriteLine(json);



var webJson = JsonSerializer.Serialize(
    new { UserName = "gsw", Password = "123456" },
    JsonSerializerOptions.Web // Defaults to camelCase naming policy.
    );
Console.WriteLine(webJson);


string sourceText = """
Lorem ipsum dolor sit amet, consectetur adipiscing elit.
Sed non risus. Suspendisse lectus tortor, dignissim sit amet, 
adipiscing nec, ultricies sed, dolor. Cras elementum ultrices amet diam.
""";
Console.WriteLine(sourceText);

var KVs = sourceText
    .Split(new char[] { ' ', '.', ',', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
    .Select(word => word.ToLowerInvariant())
    .CountBy(word => word);
foreach (var item in KVs)
{
    Console.WriteLine(item.Key + ":" + item.Value);
}
Console.WriteLine("-------------------------");
var mostFrequentWord = KVs.MaxBy(pair => pair.Value);
Console.WriteLine(mostFrequentWord.Key + ":" + mostFrequentWord.Value);//amet:3
Console.WriteLine("-------------------------");
mostFrequentWord = KVs.MinBy(pair => pair.Value);
Console.WriteLine(mostFrequentWord.Key + ":" + mostFrequentWord.Value);//lorem:1
Console.WriteLine("-------------------------");
var orders = new Order[] {
       new Order("zs", 1,DateTime.Now),
       new Order("ls", 2,DateTime.Now),
       new Order("ww", 3,DateTime.Now),
       new Order("zs", 4,DateTime.Now),
       new Order("ls", 5,DateTime.Now),
       new Order("ww", 6,DateTime.Now),
    };
var kvs = orders.AggregateBy(
     keySelector: word => word.OrderUser,
        seed: 0m,
        (orderAmount, order) => orderAmount + order.OrderAmount
        );
foreach (var item in kvs)
{
    Console.WriteLine(item.Key + ":" + item.Value);
}

record Order(string OrderUser, decimal OrderAmount, DateTime OrderTime);