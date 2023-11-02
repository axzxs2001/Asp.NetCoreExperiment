using Dapper;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var conn = new MySqlConnection(@"server=;uid=;pwd=;database=");
var list = conn.Query<CaseTest>("SELECT * FROM ligui.test_case;").ToList();
builder.Services.AddSingleton(list);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseMiddleware<MiddlewareTest>();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
internal class MiddlewareTest
{
    RequestDelegate _next;
    public MiddlewareTest(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, List<CaseTest> list)
    {

        await context.Response.WriteAsync("MiddlewareTest");
    }

}

class CaseTest
{
    public string Name { get; set; }
    public int Plan_ID { get; set; }

}