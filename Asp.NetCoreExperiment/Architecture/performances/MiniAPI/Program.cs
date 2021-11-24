using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapGet("/test01", () =>
{
    var arr = new string[]
   {
    "1","2","3","4","5","6","7","8","9","0"
   };
    var index = RandomNumberGenerator.GetInt32(arr.Length);
    return arr[index];
});
app.Run();

