
while (true)
{
    Console.WriteLine("请输入干别的事的秒数：");
    var second = int.Parse(Console.ReadLine()!);
    Console.WriteLine(await GetTest(second));
    Console.WriteLine("回车继续");
    Console.ReadLine();
    Console.WriteLine("--------------------------");
}

async Task<string> GetTest(int second)
{
    var client = new HttpClient();
    var begintTime = DateTime.Now;
    Console.WriteLine($"非await调用Web方法前：{begintTime:yyyy-MM-dd HH:mm:ss},为以后计算的开始时间");
    Task<string> getStringTask =
        client.GetStringAsync("http://localhost:5000/weatherforecast");
    Console.WriteLine($"非await调用Web方法后,DoSomething前：{DateTime.Now:yyyy-MM-dd HH:mm:ss}，与开始时差：{(DateTime.Now - begintTime).TotalMilliseconds}毫秒");
    DoSomething(second);
    Console.WriteLine($"DoSomething后：{DateTime.Now:yyyy-MM-dd HH:mm:ss}，与开始时差：{(DateTime.Now - begintTime).TotalSeconds}秒");
    string contents = await getStringTask;
    Console.WriteLine($"await Web结束 后：{DateTime.Now:yyyy-MM-dd HH:mm:ss}，与开始时差：{(DateTime.Now - begintTime).TotalSeconds}秒");
    return contents;
}

void DoSomething(int second)
{
    Thread.Sleep(second * 1000);
    Console.WriteLine($"DoOther花了{second}秒来干别的事");
}