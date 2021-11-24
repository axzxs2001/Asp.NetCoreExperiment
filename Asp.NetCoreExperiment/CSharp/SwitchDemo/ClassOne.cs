using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchDemo;

public class ClassOne
{
    public void Run()
    {
        var entity = new YamlFormatCreater();
        var data = new Data();
        Console.WriteLine(GetData(entity, data));
    }
    public string GetData(IFormatCreater entity, Data data) => entity switch
    {
        CSVFormatCreater csvFormatCreater => csvFormatCreater.ToCSV(data),
        JsonFormatCreater jsonFormatCreater => jsonFormatCreater.ToJson(data),
        XMLFormatCreater xmlFormatCreater => xmlFormatCreater.ToXML(data),
        YamlFormatCreater yamlFormatCreater => yamlFormatCreater.ToYAML(data),
        _ => "this format is not adapted"
    };
}

public class Data
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public string? Model { get; set; }
}
public interface IFormatCreater
{ }
public class CSVFormatCreater : IFormatCreater
{
    public string ToCSV(Data data)
    {
        return "To CSV";
    }
}
public class JsonFormatCreater : IFormatCreater
{
    public string ToJson(Data data)
    {
        return "To JSON";
    }
}
public class XMLFormatCreater : IFormatCreater
{
    public string ToXML(Data data)
    {
        return "To XML";
    }
}
public class YamlFormatCreater : IFormatCreater
{
    public string ToYAML(Data data)
    {
        return "To YAML";
    }
}