using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();

builder.Services.AddIdentityCore<IdentityUser>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddApiEndpoints();

var dbPath = string.Format("Data Source={0}\\db.sqlite", Directory.GetCurrentDirectory());
builder.Services.AddDbContext<IdentityDbContext>(options => options.UseSqlite(dbPath));


var app = builder.Build();
app.MapIdentityApi<IdentityUser>();

app.MapGet("/", (ClaimsPrincipal user) => $"»¶Ó­£º {user.Identity!.Name}").RequireAuthorization();


app.Run();



//class MyDbContext : IdentityDbContext<IdentityUser>
//{
//    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
//    { }  
//}