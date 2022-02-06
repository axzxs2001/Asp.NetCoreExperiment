using Microsoft.AspNetCore.Cors;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    //options.AddPolicy(name: "Policy1",
    //        builder =>
    //        {
    //            builder
    //            .WithOrigins("http://localhost:5280")               
    //            .AllowAnyMethod()
    //            .AllowAnyHeader()
    //            ;
    //        });

    //options.AddPolicy(name: "Policy2",
    //            builder =>
    //            {
    //                builder
    //                .WithOrigins("http://localhost:5280")
    //                .WithMethods("PUT")
    //                ;
    //            });
    //options.AddPolicy(name: "Policy3",
    //        builder =>
    //        {
    //            builder.WithOrigins("http://localhost:5280")
    //            .WithHeaders("x-cors-header")
    //            .AllowAnyMethod()
    //            ;
    //        });
    options.AddPolicy(name: "Policy4",
            builder =>
            {
                builder.WithOrigins("http://localhost:5280")
                //.AllowCredentials()
                .AllowAnyMethod()
                ;
            });
});

var app = builder.Build();

app.UseCors();

//app.MapGet("/test1", () => "get的结果");
//app.MapPost("/test1", () => "post的结果");
//app.MapDelete("/test1", [DisableCors]() => "delete的结果");
//app.MapPut("/test1", () => "put的结果");
//app.Map("/test1", () =>
//{
//   return "map全部";
//});

//app.MapGet("/test2", [EnableCors("Policy2")] () => "get的结果");
//app.MapPost("/test2", [EnableCors("Policy2")] () => "post的结果");
//app.MapDelete("/test2", [EnableCors("Policy2")] () => "delete的结果");
//app.MapPut("/test2", [EnableCors("Policy2")] () => "put的结果");
//app.Map("/test2", [EnableCors("Policy2")] () => "map全部");

//app.MapGet("/test3", [EnableCors("Policy3")] () => "get的结果");
//app.MapPost("/test3", [EnableCors("Policy3")] () => "post的结果");
//app.MapDelete("/test3", [EnableCors("Policy3")] () => "delete的结果");
//app.MapPut("/test3", [EnableCors("Policy3")] () => "put的结果");
//app.Map("/test3", [EnableCors("Policy3")] () => "map全部");

app.MapGet("/test4", [EnableCors("Policy4")] () => "get的结果");
app.MapPost("/test4", [EnableCors("Policy4")] () => "post的结果");
app.MapDelete("/test4", [EnableCors("Policy4")] () => "delete的结果");
app.MapPut("/test4", [EnableCors("Policy4")] () => "put的结果");
app.Map("/test4", [EnableCors("Policy4")] () => "map全部");
app.Run();

