
var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(100));
while (true)
{
    Console.WriteLine(DateTime.Now.ToString("O"));
    await timer.WaitForNextTickAsync();
}
    



var score = 90;
switch (score)
{
    case > 100 or < 0:
        Console.WriteLine("错误");
        break;
    case 100:
        Console.WriteLine("满分");
        break;
    case < 100 and >= 90:
        Console.WriteLine("优秀");
        break;
    case < 90 and >= 60:
        Console.WriteLine("极格");
        break;
    case < 60:
        Console.WriteLine("不极格");
        break;
}


var v = A.GetValue() switch
{
    int i => i,
    double d => d,
    _ => 2f
};


class A
{
    public static dynamic GetValue()
    {
        if (DateTime.Now.Second % 2 == 0)
        {
            return 1;
        }
        else
        {
            return 1.1;
        }
    }
}