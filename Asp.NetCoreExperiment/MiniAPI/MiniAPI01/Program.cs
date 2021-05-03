using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

var app = WebApplication.Create();

app.MapGet("/ping", http => http.Response.WriteAsJsonAsync(new { message = "pong" }));

await app.RunAsync();























//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Http;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//var app = WebApplication.Create();
//app.MapGet("/", GetPerson);
//await app.RunAsync();


//static async Task GetPerson(HttpContext http)
//{
//    var list = new List<dynamic> { new { id = 1, name = "уехЩ", sex = true, age = 20 } };
//    await http.Response.WriteAsJsonAsync(list);
//}