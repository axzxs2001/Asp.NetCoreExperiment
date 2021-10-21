
using Dapr.Actors;
using Dapr.Actors.Client;


Console.WriteLine("回车开始");

Console.ReadLine();
var actorType = "OrderFactorActor";
var actorId = new ActorId("1");

var factory = new ActorProxyFactory(new ActorProxyOptions { HttpEndpoint = "http://localhost:3999" });
var proxy = factory.CreateActorProxy<IOrderFactoryActory.Interfaces.IOrderFactoryActory>(actorId, actorType);
//var proxy = ActorProxy.Create<IOrderFactoryActory.Interfaces.IOrderFactoryActory>(actorId, actorType, new ActorProxyOptions
//{
//    HttpEndpoint = "http://localhost:3999"
//});



new Thread(CallHttp).Start();
new Thread(CallHttp).Start();


new Thread(CallActor).Start(proxy);
new Thread(CallActor).Start(proxy);

Console.WriteLine("回车结束");
Console.ReadLine();

static void SetGetState()
{
    //Console.WriteLine($"Calling SetDataAsync on {actorType}:{actorId}...");
    //var response = await proxy.SetOrderAsync(new IOrderFactoryActory.Interfaces.Order()
    //{
    //    OrderNo = "0001",
    //    OrderType = "B",
    //    Amount = amount,
    //    Quantity = 20

    //});
    //Console.WriteLine($"Set response: {response}");

    //Console.WriteLine($"Calling GetDataAsync on {actorType}:{actorId}...");
    //var order = await proxy.GetOrderAsync();
    //Console.WriteLine($"Got response: {order}");
}

static void Call(IOrderFactoryActory.Interfaces.IOrderFactoryActory proxy)
{
    var amount = proxy.GetOrderAmountAsync(new IOrderFactoryActory.Interfaces.Order()
    {
        OrderNo = "0001",
        OrderType = "B",
        Amount = DateTime.Now.Millisecond,
        Quantity = 20

    }).Result;
    Console.WriteLine($"计算结果：{amount}");
}
static void CallActor(object? obj)
{
    var proxy = obj as IOrderFactoryActory.Interfaces.IOrderFactoryActory;
    var time = proxy.GetTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")).Result;
    Console.WriteLine(time);
}
static void CallHttp()
{
    var client = new HttpClient();
    Console.WriteLine(client.GetStringAsync("http://localhost:5000/gettime?intime=" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")).Result);
}