using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;

F0(1, 2, 3, 4);
F1(1, 2, 3, 4);
F2(1, 2, 3, 4);
F3(1, 2, 3, 4);
F4(1, 2, 3, 4);
F5(1, 2, 3, 4);

var email = new EmailDemo();
email.SendEmail("abc@aaa");
email.EMail = "abc@gmail.com";


static void F0(params int[] arr)
{
    foreach (var item in arr)
    {
        Console.WriteLine(item);
    }
}
static void F1(params List<int> list)
{
    foreach (var item in list)
    {
        Console.WriteLine(item);
    }
}
static void F2(params IList<int> list)
{
    foreach (var item in list)
    {
        Console.WriteLine(item);
    }
}
static void F3(params IEnumerable<int> list)
{
    foreach (var item in list)
    {
        Console.WriteLine(item);
    }
}
static void F4(params Span<int> list)
{
    foreach (var item in list)
    {
        Console.WriteLine(item);
    }
}
static void F5(params ReadOnlySpan<int> list)
{
    foreach (var item in list)
    {
        Console.WriteLine(item);
    }
}


public partial class EmailDemo
{
    [GeneratedRegex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex ValidEMail();

    public void SendEmail(string email)
    {
        if (!ValidEMail().IsMatch(email))
        {
            Console.WriteLine("emal不正确");
            return;
        }
        Console.WriteLine("发送EMail");
    }

    [GeneratedRegex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
    private static partial Regex ValidateEMail { get; }

    private string _email;
    public string EMail
    {
        get
        {
            return _email;
        }
        set
        {
            if (ValidateEMail.IsMatch(value))
            {
                _email = value;
            }
            else
            {
                Console.WriteLine("emal不正确");
            }
        }
    }
}

