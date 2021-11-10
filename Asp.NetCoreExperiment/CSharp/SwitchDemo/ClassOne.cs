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
        var entity = new CSVFormatCreater();
        var data = new Data();
        GetData(entity, data);

    }
    public string GetData(IFormatCreater entity, Data data) => entity switch
    {
        CSVFormatCreater csvFormatCreater => csvFormatCreater.ToCSV(data),
        JsonFormatCreater jsonFormatCreater => jsonFormatCreater.ToJson(data),
        XMLFormatCreater xmlFormatCreater => xmlFormatCreater.ToXML(data),
        _ => "都不是"
    };
}

public class Data
{
}
public interface IFormatCreater
{ }
public class CSVFormatCreater : IFormatCreater
{
    public string ToCSV(Data data)
    {
        return "Entity01.Method01";
    }
}
public class JsonFormatCreater : IFormatCreater
{
    public string ToJson(Data data)
    {
        return "Entity02.Method02";
    }
}
public class XMLFormatCreater : IFormatCreater
{
    public string ToXML(Data data)
    {
        return "Entity03.Method03";
    }
}