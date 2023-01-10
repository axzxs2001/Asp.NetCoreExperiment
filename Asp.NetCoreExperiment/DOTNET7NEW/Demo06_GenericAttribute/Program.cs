using System.Reflection;
using System.Text;


var person = new Person();
person.ID = 10;
person.Name = "桂素伟";
Console.WriteLine(person);

var order = new Order();
order.ID = 10;
order.Name = "批发";
order.Pirce = 12.34m;
Console.WriteLine(order);

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class DataFormatAttribute<T> : Attribute
{
}
public interface IFormatter
{
    string ConvertTo(object obj);
}

public class JsonFormatter : IFormatter
{
    public string ConvertTo(object obj)
    {
        return System.Text.Json.JsonSerializer.Serialize(obj);
    }
}
public class XmlFormatter : IFormatter
{
    public string ConvertTo(object obj)
    {
        var ser = new System.Xml.Serialization.XmlSerializer(obj.GetType());
        using var memory = new MemoryStream();
        ser.Serialize(memory, obj);
        var bytes = memory.GetBuffer();
        return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
    }
}

public abstract class Entity
{
    public override string? ToString()
    {
        var type = this.GetType();
        foreach (var attr in type.GetCustomAttributes(false))
        {
            if (attr.GetType().IsGenericType)
            {
                var pars = attr.GetType().GenericTypeArguments;
                foreach (var par in pars)
                {
                    var format = Activator.CreateInstance(par) as IFormatter;
                    return format?.ConvertTo(this);
                }
            }
        }
        return null;
    }
}

[DataFormat<JsonFormatter>()]
public class Person : Entity
{
    public int ID { get; set; }
    public string? Name { get; set; }
}
[DataFormat<XmlFormatter>()]
public class Order : Entity
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public decimal Pirce { get; set; }
}