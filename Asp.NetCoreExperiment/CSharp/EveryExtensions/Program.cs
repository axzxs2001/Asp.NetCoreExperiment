






Console.WriteLine(2024.November(12));








static class DateTimeExtensions
{
    public static DateTime November(this int Year, int day)
    {
        return new DateTime(Year, 11, day);
    }
}