using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = new PascalNamingPolicy();

}); 

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


public class PascalNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        return name;
    }
}