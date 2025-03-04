

using System.Text.Json;
using System.Text.Json.Serialization;

var ceo = new Employee { NO = 1, Name = "CEO" };
ceo.Manager = ceo;
//Console.WriteLine(JsonSerializer.Serialize(ceo));
Console.WriteLine(JsonSerializer.Serialize(ceo, ContextWithPreserveReference.Default.Employee));
Console.WriteLine("---------------");

var manager = new Employee { NO = 2, Name = "Manager" };
manager.Manager = ceo;
// Console.WriteLine(JsonSerializer.Serialize(manager));
 Console.WriteLine(JsonSerializer.Serialize(manager, ContextWithPreserveReference.Default.Employee));
Console.WriteLine("---------------");

var employee = new Employee { NO = 3, Name = "Employee" };
employee.Manager = manager;
//Console.WriteLine(JsonSerializer.Serialize(employee));
Console.WriteLine(JsonSerializer.Serialize(employee, ContextWithPreserveReference.Default.Employee));
Console.ReadLine();


[JsonSourceGenerationOptions(ReferenceHandler = JsonKnownReferenceHandler.Preserve)]
[JsonSerializable(typeof(Employee))]
internal partial class ContextWithPreserveReference : JsonSerializerContext
{
}

class Employee
{
    public int NO { get; set; }
    public string? Name { get; set; }
    public Employee? Manager { get; set; }
}