
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

//builder.Authentication.AddJwtBearer();
#region ����
builder.Authentication.AddJwtBearer(opt =>
{
    //token��֤����
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("1234567890abcdefg")),
        ValidateIssuer = true,
        ValidIssuer = "http://localhost:5274",
        ValidateAudience = true,
        ValidAudience = "http://localhost:5274",
        ClockSkew = TimeSpan.Zero,
        RequireExpirationTime = true,       
    }; ;
});

builder.Services
    .AddAuthorization(options =>
    {
        //��Ӳ�������
        options.AddPolicy("Permission", policyBuilder => policyBuilder.AddRequirements(new PermissionRequirement()));
    })
    .AddSingleton(new List<Permission> { new Permission { RoleName = "admin", Url = "/Policy", Method = "get" } })
    .AddSingleton<IAuthorizationHandler, PermissionHandler>();
#endregion



var app = builder.Build();

#region ����
app.MapGet("/login", () =>
{
    //��JWTSecurityTokenHandler����token
    return new JwtSecurityTokenHandler().WriteToken(
        new JwtSecurityToken(
            issuer: "http://localhost:5274",
            audience: "http://localhost:5274",
            claims: new Claim[] {
                new Claim(ClaimTypes.Role, "admin"),
                new Claim(ClaimTypes.Name, "����ΰ")
            },
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddSeconds(500000),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes("1234567890abcdefg")),
                SecurityAlgorithms.HmacSha256)
            )
        );
});
#endregion

//app.MapGet("/", () => "����֤");

//app.MapGet("/myhome", (ClaimsPrincipal user) => $"��� {user.Identity?.Name}����ӭ���������ҳ")
//    .RequireAuthorization();

//app.MapGet("/order", (ClaimsPrincipal user) => $"�û���  {user.Identity?.Name}�����ǣ�{user.Claims?.Where(s => s.Type == ClaimTypes.Role).First().Value}��ɫ���������ר��ҳ").RequireAuthorization(builder =>
//{
//    builder.RequireRole("admin");
//});

#region ����

app.MapGet("/policy", (ClaimsPrincipal user) => $"Hello �û���{user.Identity?.Name}, ��ɫ��{user.Claims?.Where(s => s.Type == ClaimTypes.Role).First().Value}. This is a policy!").RequireAuthorization("Permission");
#endregion

app.Run();


#region ����
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