using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();

app.UseStaticFiles();
app.MapGet("/test", (HttpContext context) =>
{
    var client = new HttpClient();
    //client.BaseAddress = new Uri(@"https://trydotnet.microsoft.com/");
    //var r = client.GetAsync("v2/editor?waitForConfiguration=true&instanceId=example").Result;
    //context.Response.WriteAsync(r.Content.ReadAsStringAsync().Result).Wait();

    var aa = """
              {
                 type: "setWorkspace",
                 workspace: {
                     workspaceType: "console",
                     buffers: [{
                         id: "Program.cs",
                         content: "using System;\n\npublic class Program\n{\n    public static void Main()\n    {\n        Console.WriteLine(\"Hello World!\");\n    }\n}"
                     }]
                 },
                 bufferId: "Program.cs"
             }

             """;

    client.BaseAddress = new Uri(@"https://trydotnet.microsoft.com");
    var rr = client.PostAsync("", new StringContent(System.Text.Json.JsonSerializer.Serialize(aa))).Result;
    context.Response.WriteAsync(rr.Content.ReadAsStringAsync().Result).Wait();

});


app.MapPost("/test", (HttpContext context) =>
{
    //var client = new HttpClient();
    //client.BaseAddress = new Uri(@"https://trydotnet.microsoft.com/");
    //var r = client.PostAsync("",new StringContent(System.Text.Json.JsonSerializer.Serialize(aa))).Result;
    //context.Response.WriteAsync(r.Content.ReadAsStringAsync().Result).Wait();

});


app.Run();


class AA
{
    public string type { get; set; }
    public W workspace { get; set; }
    public string bufferId { get; set; }


}
class W
{
    public string workspaceType { get; set; }
    public B[] buffers { get; set; }
}
class B
{
    public string id { get; set; }
    public string content { get; set; }
}