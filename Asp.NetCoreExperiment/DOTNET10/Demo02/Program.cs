// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


void SetPerson(Person? per, string name, int age)
{
    per.Name = name;
    per?.Age = age;
}

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}