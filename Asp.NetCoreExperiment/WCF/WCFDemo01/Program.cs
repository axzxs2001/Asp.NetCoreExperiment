using CoreWCF;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using CoreWCF.Configuration;
using CoreWCF.Description;


var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel((context, options) =>
{
    options.AllowSynchronousIO = true;
});

builder.Services.AddServiceModelServices().AddServiceModelMetadata();
builder.Services.AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();

var app = builder.Build();
app.UseServiceModel(builder =>
{
    builder
    .AddService(typeof(ToDoService))
    .AddServiceEndpoint<ToDoService, IToDoService>(new BasicHttpBinding(), "/ToDoService/basichttp")
    .AddServiceEndpoint<ToDoService, IToDoService>(new WSHttpBinding(SecurityMode.Transport), "/ToDoService/WSHttps");
});

var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
serviceMetadataBehavior.HttpGetEnabled = true;

app.Run();


[DataContract]
public record Item
{
    [DataMember]
    public string? Title { get; set; }
    [DataMember]
    [AllowNull]
    public string? Description { get; set; }
    [DataMember]
    public DateTime CreateOn { get; set; }
    [DataMember]
    public bool IsComplete { get; set; } = false;
}

[ServiceContract]
public interface IToDoService
{
    [OperationContract]
    bool Add(Item item);

    [OperationContract]
    List<Item> GetList();

    [OperationContract] 
    bool Remove(string? title);

}

public class ToDoService : IToDoService
{
    static List<Item> _list = new List<Item>();

    public bool Add(Item item)
    {
        item.CreateOn = DateTime.Now;
        _list.Add(item);
        return true;
    }


    public List<Item> GetList() => _list;


    public bool Remove(string? title)
    {
        var item = _list.SingleOrDefault(s => s.Title == title);
        if (item != null)
        {
            return _list.Remove(item);
        }
        else
        {
            return false;
        }
    }
}



