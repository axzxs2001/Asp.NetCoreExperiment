using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

var app = WebApplication.Create();
app.MapGet("/", GetTodos);
await app.RunAsync();


static async Task GetTodos(HttpContext http)
{
    var list = new List<dynamic> { new { id = 1, name = "abc" } };

    await http.Response.WriteAsJsonAsync(list);
}