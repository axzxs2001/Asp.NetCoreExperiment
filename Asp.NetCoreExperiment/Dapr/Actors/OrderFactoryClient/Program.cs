using Dapr.Actors;
using Dapr.Actors.Client;
using IOrderFactoryActory.Interfaces;
using Quartz;
using Quartz.Impl;
using System.Security.Cryptography;


Console.WriteLine("回车开始");
Console.ReadLine();

var scheduler = await RunJobAsync();
var factory = new ActorProxyFactory(new ActorProxyOptions { HttpEndpoint = "http://localhost:3999" });
var accountNo = "808080808080808080";
var account = CreateActor(factory, accountNo);
var total = 0m;
while (true)
{
    var tasks = new List<Task>();
    for (var i = 0; i < 50; i++)
    {
        var amount = RandomNumberGenerator.GetInt32(100, 5000);
        var chargeTask = new Task(async () =>
        {
            var balance = await account.ChargeAsync(amount);
            Console.WriteLine($"****   账户：{accountNo}  本次存款：{amount}  缓存余额：{balance}   ****");
        });
        tasks.Add(chargeTask);
        total += amount;
    }

    foreach (var task in tasks)
    {
        task.Start();
    }

    Console.WriteLine($"全部存款汇总：{total}");   
    Console.WriteLine("回车继续发一批，退出按E");
    if (Console.ReadLine() == "E")
    {
        break;
    }
}
await scheduler.Shutdown();



static IAccountActor CreateActor(ActorProxyFactory factory, string accountNo)
{
    var actorType = "AccountActor";
    var actorId = new ActorId(accountNo);
    return factory.CreateActorProxy<IAccountActor>(actorId, actorType);
}
static async Task<IScheduler> RunJobAsync()
{
    var factory = new StdSchedulerFactory();
    var scheduler = await factory.GetScheduler();
    await scheduler.Start();
    var job = JobBuilder.Create<SavaAccountJob>()
        .WithIdentity("SavaAccountJob", "SavaAccountGroup")
        .Build();

    var trigger = TriggerBuilder.Create()
        .WithIdentity("SavaAccountTrigger", "SavaAccountGroup")
        .StartNow()
        .WithSimpleSchedule(x => x
            .WithIntervalInSeconds(10)
            .RepeatForever())
        .Build();
    await scheduler.ScheduleJob(job, trigger);
    return scheduler;
}

class SavaAccountJob : IJob
{
    static decimal total = 0;
    public async Task Execute(IJobExecutionContext context)
    {
        var accountNo = "808080808080808080";
        var actorType = "AccountActor";
        var actorId = new ActorId(accountNo);
        var factory = new ActorProxyFactory(new ActorProxyOptions { HttpEndpoint = "http://localhost:3999" });
        var account = factory.CreateActorProxy<IAccountActor>(actorId, actorType);
        var balance = await account.ChargeAsync(0m);
        total += balance;
        var newBalance = account.ChargeAsync(-balance).Result;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"账户：{accountNo}  处理余额：{balance} 定时处理完后余额：{newBalance} 总体余额：{total}");
        Console.ResetColor();
    }
}



//static async Task TimeDemo()
//{
//    Console.WriteLine("回车开始");
//    Console.ReadLine();

//    //调用api是并行的
//    var client = new HttpClient();
//    var httpTask1 = new Task(async () =>
//    {
//        Console.WriteLine(await client.GetStringAsync("http://localhost:5000/gettime?intime=" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
//    });
//    var httpTask2 = new Task(async () =>
//    {
//        Console.WriteLine(await client.GetStringAsync("http://localhost:5000/gettime?intime=" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
//    });
//    httpTask1.Start();
//    httpTask2.Start();

//    //相同ID的actor是串行的，不同ID的actor是并行的
//    var factory = new ActorProxyFactory(new ActorProxyOptions { HttpEndpoint = "http://localhost:3999" });
//    var account1 = CreateActor(factory, "11111111111");
//    var account2 = CreateActor(factory, "22222222222");
//    var actorTask1_1 = new Task(async () =>
//    {
//        Console.WriteLine(await account1.GetTimeAsync(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
//    });
//    var actorTask1_2 = new Task(async () =>
//    {
//        Console.WriteLine(await account1.GetTimeAsync(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
//    });
//    var actorTask2 = new Task(async () =>
//    {
//        Console.WriteLine(await account2.GetTimeAsync(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
//    });
//    actorTask1_1.Start();
//    actorTask1_2.Start();
//    actorTask2.Start();

//    Console.WriteLine("回车结束");
//    Console.ReadLine();

//}





//static async Task SetGetState()
//{
//    var actorType = "OrderFactorActor";
//    var actorId = new ActorId("1");
//    var factory = new ActorProxyFactory(new ActorProxyOptions { HttpEndpoint = "http://localhost:3999" });
//    var proxy = factory.CreateActorProxy<IOrderFactoryActory.Interfaces.IOrderFactoryActory>(actorId, actorType);
//    Console.WriteLine($"Calling SetDataAsync on {actorType}:{actorId}...");
//    var response = await proxy.SetOrderAsync(new IOrderFactoryActory.Interfaces.Order()
//    {
//        OrderNo = "0001",
//        OrderType = "B",
//        Amount = 3000,
//        Quantity = 20

//    });
//    Console.WriteLine($"Set response: {response}");

//    Console.WriteLine($"Calling GetDataAsync on {actorType}:{actorId}...");
//    var order = await proxy.GetOrderAsync();
//    Console.WriteLine($"Got response: {order}");
//}

//static void Call(IOrderFactoryActory.Interfaces.IOrderFactoryActory proxy)
//{
//    var amount = proxy.GetOrderAmountAsync(new IOrderFactoryActory.Interfaces.Order()
//    {
//        OrderNo = "0001",
//        OrderType = "B",
//        Amount = DateTime.Now.Millisecond,
//        Quantity = 20

//    }).Result;
//    Console.WriteLine($"计算结果：{amount}");
//}
