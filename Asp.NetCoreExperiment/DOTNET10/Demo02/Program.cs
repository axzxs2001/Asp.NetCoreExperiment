//不支持nameof(List<>)
Console.WriteLine(nameof(List<>));
Console.WriteLine(nameof(List<string>));
Console.WriteLine(nameof(List<int>));


//您可以向 lambda 表达式参数添加参数修饰符，例如scoped、ref、in、out或 ，ref readonly而无需指定参数类型：
TryParse<int> parse1 = (text, out result) => Int32.TryParse(text, out result);

//之前写法
TryParse<int> parse2 = (string text, out int result) => Int32.TryParse(text, out result);


void SetPersonName(Person? person, string name)
{
    //空条件赋值
    person?.Name = name;
}

delegate bool TryParse<T>(string text, out T result);

public partial class Person
{
    public string Name
    {
        get;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("Name cannot be null or empty");
            }
            field = value;
        }
    }
    public int Age
    {
        get;
        set
        {
            if (150 >= field && field >= 0)
            {
                field = value;
            }
            else
            {
                throw new Exception("Invalid age");
            }
        }
    }
    //实例构造函数和事件可以声明为部分成员
    public partial event EventHandler? NameChanged;
    public partial Person();
}

public partial class Person
{
    public partial Person()
    {
        Name = "Default Name";
        Age = 0;
    }

    private EventHandler _nameChangedHandlers;
    // 实现声明：必须包含 add 和 remove
    public partial event EventHandler NameChanged
    {
        add
        {
            Console.WriteLine("Handler added.");
            _nameChangedHandlers += value;
        }
        remove
        {
            Console.WriteLine("Handler removed.");
            _nameChangedHandlers -= value;
        }
    }
    public void OnNameChanged()
    {
        _nameChangedHandlers?.Invoke(this, EventArgs.Empty);
    }
}