using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var authConfig = new AuthConfig();
builder.Configuration.Bind("AuthConfig", authConfig);
builder.Services.AddSingleton(authConfig);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authConfig.SecurityKey)),
        ValidateIssuer = true,
        ValidIssuer = authConfig.Issuer,
        ValidateAudience = true,
        ValidAudience = authConfig.Audience,
        ClockSkew = TimeSpan.Zero,
        RequireExpirationTime = true,
        ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha512 }
    };
});
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("rolevalidate", policy => policy.RequireRole("user"));
});

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/login", ([FromServices] AuthConfig config,  [FromBody] UserModel userModel) =>
{
    if (userModel.UserName != "gsw" || userModel.Password != "111111")
    {
        return new { result = false, message = "用户名或密码错误！", token = "" };
    }
    else
    {
        var token = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
            issuer: config.Issuer,
            audience: config.Audience,
            claims: new[] {
               new Claim(ClaimTypes.Role, "user")
            },
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddSeconds(config.Expires),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.SecurityKey)), SecurityAlgorithms.HmacSha512)
            ));
        return new { result = true, message = "", token = token };
    }
}).AllowAnonymous();

app.MapGet("/index", () =>
{
    return "登录成功！";
}).RequireAuthorization("rolevalidate");

app.Run();



public class AuthConfig
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int Expires { get; set; }
    public string SecurityKey { get; set; }
}
public class UserModel
{
    public string UserName { get; set; }
    public string Password { get; set; }
}