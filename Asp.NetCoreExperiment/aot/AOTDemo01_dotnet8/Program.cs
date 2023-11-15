using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateSlimBuilder(args);
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

#region 策略

builder.Services
    .AddAuthorization(options =>
    {
        //添加策略名称
        options.AddPolicy("Permission", policyBuilder => policyBuilder.AddRequirements(new PermissionRequirement()));
    })
.AddSingleton(new List<Permission> {
    new Permission { RoleName = "admin", Url = "/Policy", Method = "get" },
    new Permission { RoleName = "admin", Url = "/todos", Method = "get" },
})
.AddSingleton<IAuthorizationHandler, PermissionHandler>()
.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(opt =>
{
    //token验证参数
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("1234567890abcdefg1234567890abcdefg")),
        ValidateIssuer = true,
        ValidIssuer = "http://localhost:5274",
        ValidateAudience = true,
        ValidAudience = "http://localhost:5274",
        ClockSkew = TimeSpan.Zero,
        RequireExpirationTime = true,
    };
});
#endregion



var app = builder.Build();

var sampleTodos = new Todo[] {
    new(1, "Walk the dog"),
    new(2, "Do the dishes", DateOnly.FromDateTime(DateTime.Now)),
    new(3, "Do the laundry", DateOnly.FromDateTime(DateTime.Now.AddDays(1))),
    new(4, "Clean the bathroom"),
    new(5, "Clean the car", DateOnly.FromDateTime(DateTime.Now.AddDays(2)))
};

var todosApi = app.MapGroup("/todos");
todosApi.MapGet("/", () => sampleTodos).RequireAuthorization("Permission"); ;
todosApi.MapGet("/{id}", (int id) =>
    sampleTodos.FirstOrDefault(a => a.Id == id) is { } todo
        ? Results.Ok(todo)
        : Results.NotFound()).RequireAuthorization("Permission");


#region 策略
app.MapGet("/login", () =>
{
    //用JWTSecurityTokenHandler生成token
    return new JwtSecurityTokenHandler().WriteToken(
        new JwtSecurityToken(
            issuer: "http://localhost:5274",
            audience: "http://localhost:5274",
            claims: new Claim[] {
                new Claim(ClaimTypes.Role, "admin"),
                new Claim(ClaimTypes.Name, "桂素伟")
            },
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddSeconds(500000),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes("1234567890abcdefg1234567890abcdefg")),
                SecurityAlgorithms.HmacSha256Signature)
            )
        );
});
#endregion

#region 策略

app.MapGet("/policy", (ClaimsPrincipal user) => $"Hello 用户：{user.Identity?.Name}, 角色：{user.Claims?.Where(s => s.Type == ClaimTypes.Role).First().Value}. This is a policy!").RequireAuthorization("Permission");
#endregion


app.Run();

public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);

[JsonSerializable(typeof(Todo[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}




#region 策略
public class PermissionRequirement : IAuthorizationRequirement
{
}
public class Permission
{
    public string? RoleName { get; set; }
    public string? Url { get; set; }
    public string? Method { get; set; }
}
public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly List<Permission> _userPermissions;
    public PermissionHandler(List<Permission> permissions)
    {
        _userPermissions = permissions;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        if (context.Resource is DefaultHttpContext)
        {
            var httpContext = context.Resource as DefaultHttpContext;
            var questPath = httpContext?.Request?.Path;
            var method = httpContext?.Request?.Method;
            var isAuthenticated = context?.User?.Identity?.IsAuthenticated;
            if (isAuthenticated.HasValue && isAuthenticated.Value)
            {
                var role = context?.User?.Claims?.SingleOrDefault(s => s.Type == ClaimTypes.Role)?.Value;
                if (_userPermissions.Where(w => w.RoleName == role && w.Method?.ToUpper() == method?.ToUpper() && w.Url?.ToLower() == questPath).Count() > 0)
                {
                    context?.Succeed(requirement);
                }
                else
                {
                    context?.Fail();
                }
            }
        }
        return Task.CompletedTask;
    }
}
#endregion