using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder();

var jwtConfig = new JWTConfig();
builder.Configuration.GetSection("JWTConfig").Bind(jwtConfig);
builder.Services.AddSingleton(jwtConfig);

builder.Services.AddSingleton(new List<Permission> { new Permission { RoleName = "admin", Url = "/helloadmin", Method = "get" } });
builder.Services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

builder.Services
    .AddAuthorization(options =>
    {
        var permissionRequirement = new PermissionRequirement();
        options.AddPolicy("Permission", policy => policy.AddRequirements(permissionRequirement));
    })
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
    {
        opt.RequireHttpsMetadata = false;
        opt.TokenValidationParameters = JwtToken.CreateTokenValidationParameters(jwtConfig);
    });

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/hellosystem", (ILogger<Program> logger, HttpContext context) =>
{
    var message = $"hello,system,{context.User?.Identity?.Name}";
    logger.LogInformation(message);

    return message;
}).RequireAuthorization("Permission");

app.MapGet("/helloadmin", (ILogger<Program> logger, HttpContext context) =>
{
    var message = $"hello,admin,{context.User?.Identity?.Name}";
    logger.LogInformation(message);
    return message;
}).RequireAuthorization("Permission");

app.MapGet("/helloall", (ILogger<Program> logger, HttpContext context) =>
{
    var message = $"hello,all roles,{context.User?.Identity?.Name}";
    logger.LogInformation(message);
    return message;
}).RequireAuthorization("Permission");


app.MapPost("/login", [AllowAnonymous] (ILogger<Program> logger, LoginModel login, JWTConfig jwtConfig) =>
{
    logger.LogInformation("login");
    if (login.UserName == "gsw" && login.Password == "111111")
    {
        var now = DateTime.UtcNow;

        var claims = new Claim[] {
                new Claim(ClaimTypes.Role, "admin"),
                new Claim(ClaimTypes.Name, "¹ðËØÎ°"),
                new Claim(ClaimTypes.Sid, login.UserName),
                new Claim(ClaimTypes.Expiration, now.AddSeconds(jwtConfig.Expires).ToString())
                };
        var token = JwtToken.BuildJwtToken(claims, jwtConfig);
        return token;
    }
    else
    {
        return "username or password is error";
    }
});

app.Run();

public class LoginModel
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
}
public class JWTConfig
{
    public string? Secret { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public int Expires { get; set; }
}
public class JwtToken
{
    public static dynamic BuildJwtToken(Claim[] claims, JWTConfig jwtConfig)
    {
        var now = DateTime.UtcNow;
        var jwt = new JwtSecurityToken(
            issuer: jwtConfig.Issuer,
            audience: jwtConfig.Audience,
            claims: claims,
            notBefore: now,
            expires: now.AddSeconds(jwtConfig.Expires),
            signingCredentials: GetSigningCredentials(jwtConfig)
        );
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        var response = new
        {
            Status = true,
            AccessToken = encodedJwt,
            ExpiresIn = now.AddSeconds(jwtConfig.Expires),
            TokenType = "Bearer"
        };
        return response;
    }

    static SigningCredentials GetSigningCredentials(JWTConfig jwtConfig)
    {
        var keyByteArray = Encoding.ASCII.GetBytes(jwtConfig?.Secret!);
        var signingKey = new SymmetricSecurityKey(keyByteArray);
        return new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
    }
    public static TokenValidationParameters CreateTokenValidationParameters(JWTConfig jwtConfig)
    {
        var keyByteArray = Encoding.ASCII.GetBytes(jwtConfig?.Secret!);
        var signingKey = new SymmetricSecurityKey(keyByteArray);
        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingKey,
            ValidateIssuer = true,
            ValidIssuer = jwtConfig?.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtConfig?.Audience,
            ClockSkew = TimeSpan.Zero,
            RequireExpirationTime = true,
        };
    }
}
public class Permission
{
    public string? RoleName { get; set; }
    public string? Url { get; set; }
    public string? Method { get; set; }
}
public class PermissionRequirement : IAuthorizationRequirement
{
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