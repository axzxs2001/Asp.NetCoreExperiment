using OrderFactoryService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddActors(options =>
{
    options.HttpEndpoint = "http://localhost:3999";    
    options.Actors.RegisterActor<AccountActor>();
    options.Actors.RegisterActor<OrderFactorActor>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();

app.UseRouting();
app.UseEndpoints(endpoints =>
{   
    endpoints.MapActorsHandlers();
});
app.MapControllers();
app.Run();
