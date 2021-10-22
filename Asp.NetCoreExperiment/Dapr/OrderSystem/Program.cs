using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Net;

System.Console.WriteLine("##################" + Dns.GetHostName());
foreach (var address in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
{
    System.Console.WriteLine("##################" + address.AddressFamily.ToString());
    System.Console.WriteLine("##################" + address.ToString());
    System.Console.WriteLine("##################" + address.MapToIPv4().ToString());
}
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "OrderSystem", Version = "v1" });
});

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderSystem v1"));
}

app.UseAuthorization();

app.MapControllers();

app.Run();
