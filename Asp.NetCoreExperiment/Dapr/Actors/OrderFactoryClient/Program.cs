
using Dapr.Actors;
using Dapr.Actors.Client;


Console.WriteLine("Startup up...");
Console.ReadLine();
var actorType = "OrderFactorActor";
var actorId = new ActorId("1");

var factory = new ActorProxyFactory(new ActorProxyOptions { HttpEndpoint = "http://localhost:3999" });
var proxy = factory.CreateActorProxy<IOrderFactoryActory.Interfaces.IOrderFactoryActory>(actorId, actorType);
//var proxy = ActorProxy.Create<IOrderFactoryActory.Interfaces.IOrderFactoryActory>(actorId, actorType, new ActorProxyOptions
//{
//    HttpEndpoint = "http://localhost:3999"
//});

var amount = await proxy.GetOrderAmountAsync(new IOrderFactoryActory.Interfaces.Order()
{
    OrderNo = "0001",
    OrderType = "B",
    Amount = 1000,
    Quantity = 20

});
Console.WriteLine($"计算结果：{amount}");

Console.WriteLine($"Calling SetDataAsync on {actorType}:{actorId}...");

var response = await proxy.SetOrderAsync(new IOrderFactoryActory.Interfaces.Order()
{
    OrderNo = "0001",
    OrderType = "B",
    Amount = amount,
    Quantity = 20

});
Console.WriteLine($"Set response: {response}");

Console.WriteLine($"Calling GetDataAsync on {actorType}:{actorId}...");
var order = await proxy.GetOrderAsync();
Console.WriteLine($"Got response: {order}");
