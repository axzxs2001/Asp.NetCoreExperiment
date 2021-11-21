using SwitchDemo;
using static System.Console;


var one = new ClassOne();
one.Run();

//Demo01();
//Demo02();
//Demo03();
//Demo04();
//Demo05();
//Demo06();
//Demo07();
//Demo08();
//Demo09();


/// <summary>
/// 声明和类型模式
/// </summary>
static void Demo01()
{
    var numbers = new int[] { 10, 20, 30 };
    WriteLine(GetSourceLabel(numbers));  

    var letters = new List<char> { 'a', 'b', 'c', 'd' };
    WriteLine(GetSourceLabel(letters)); 

    int GetSourceLabel<T>(IEnumerable<T> source) => source switch
    {
        Array array => 1,
        ICollection<T> collection => 2,
        _ => 3,
    };
}
/// <summary>
/// 常量模式
/// </summary>
static void Demo02()
{
    WriteLine(GetGroupTicketPrice(1));
    decimal GetGroupTicketPrice(int visitorCount) => visitorCount switch
    {
        1 => 12.0m,
        2 => 20.0m,
        3 => 27.0m,
        4 => 32.0m,
        0 => 0.0m,
        _ => throw new ArgumentException($"Not supported number of visitors: {visitorCount}", nameof(visitorCount)),
    };
}
/// <summary>
/// 关系模式
/// </summary>
static void Demo03()
{
    WriteLine(GetCalendarSeason(new DateTime(2021, 3, 14)));  // output: spring
    WriteLine(GetCalendarSeason(new DateTime(2021, 7, 19)));  // output: summer
    WriteLine(GetCalendarSeason(new DateTime(2021, 2, 17)));  // output: winter

    string GetCalendarSeason(DateTime date) => date.Month switch
    {
        >= 3 and < 6 => "spring",
        >= 6 and < 9 => "summer",
        >= 9 and < 12 => "autumn",
        12 or (>= 1 and < 3) => "winter",
        _ => throw new ArgumentOutOfRangeException(nameof(date), $"Date with unexpected month: {date.Month}."),
    };
}
/// <summary>
/// 逻辑模式
/// </summary>
static void Demo04()
{
    WriteLine(Classify(13));  // output: High
    WriteLine(Classify(-100));  // output: Too low
    WriteLine(Classify(5.7));  // output: Acceptable

    string Classify(double measurement) => measurement switch
    {
        < -40.0 => "Too low",
        >= -40.0 and < 0 => "Low",
        >= 0 and < 10.0 => "Acceptable",
        >= 10.0 and < 20.0 => "High",
        >= 20.0 => "Too high",
        double.NaN => "Unknown",
    };
}
/// <summary>
/// 属性模式
/// </summary>
static void Demo05()
{
    WriteLine(TakeFive("Hello, world!"));  // output: Hello
    WriteLine(TakeFive("Hi!"));  // output: Hi!
    WriteLine(TakeFive(new[] { '1', '2', '3', '4', '5', '6', '7' }));  // output: 12345
    WriteLine(TakeFive(new[] { 'a', 'b', 'c' }));  // output: abc

    string TakeFive(object input) => input switch
    {
        string { Length: >= 5 } s => s.Substring(0, 5),
        string s => s,

        ICollection<char> { Count: >= 5 } symbols => new string(symbols.Take(5).ToArray()),
        ICollection<char> symbols => new string(symbols.ToArray()),

        null => throw new ArgumentNullException(nameof(input)),
        _ => throw new ArgumentException("Not supported input type."),
    };
}


/// <summary>
/// 位置模式
/// </summary>
static void Demo06()
{
    PrintIfAllCoordinatesArePositive(new Point2D(1, 2));

    string PrintIfAllCoordinatesArePositive(object point) => point switch
    {
        Point2D(> 0, > 0) p => p.ToString(),
        Point3D(> 0, > 0, > 0) p => p.ToString(),
        _ => string.Empty,
    };
}
/// <summary>
/// var 模式
/// </summary>
static void Demo07()
{
    WriteLine(Transform(new Point(1, 2)));  // output: Point { X = -1, Y = 2 }
    WriteLine(Transform(new Point(5, 2)));  // output: Point { X = 5, Y = -2 }

    Point Transform(Point point) => point switch
    {
        var (x, y) when x < y => new Point(-x, y),
        var (x, y) when x > y => new Point(x, -y),
        var (x, y) => new Point(x, y),
    };
}
/// <summary>
/// 弃元模式
/// </summary>
static void Demo08()
{
    WriteLine(GetDiscountInPercent(DayOfWeek.Friday));  // output: 5.0
    WriteLine(GetDiscountInPercent(null));  // output: 0.0
    WriteLine(GetDiscountInPercent((DayOfWeek)10));  // output: 0.0

    decimal GetDiscountInPercent(DayOfWeek? dayOfWeek) => dayOfWeek switch
    {
        DayOfWeek.Monday => 0.5m,
        DayOfWeek.Tuesday => 12.5m,
        DayOfWeek.Wednesday => 7.5m,
        DayOfWeek.Thursday => 12.5m,
        DayOfWeek.Friday => 5.0m,
        DayOfWeek.Saturday => 2.5m,
        DayOfWeek.Sunday => 2.0m,
        _ => 0.0m,
    };
}
/// <summary>
/// 带括号模式
/// </summary>
static void Demo09()
{
    WriteLine(GetAAA(1));
    WriteLine(GetAAA(1.0m));
    string GetAAA(object o) => o switch
    {
        (float or double or decimal) => "小数",
        (int or short or long) => "整数",
        (char or string) => "字符",
        _ => "无",
    };
}


public record Point2D(int X, int Y);
public record Point3D(int X, int Y, int Z);

public record Point(int X, int Y);