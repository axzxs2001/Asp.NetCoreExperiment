using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var Issuer = "https://www.just-agi.com/tran";
var Audience = "https://www.just-agi.com/tran";
var Expires = 86400;
var SecurityKey = "1234567890plmnbvc1234567890qwertyu1234567890abcdefg1234567890abcdefg";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecurityKey)),
        ValidateIssuer = true,
        ValidIssuer = Issuer,
        ValidateAudience = true,
        ValidAudience = Audience,
        ClockSkew = TimeSpan.Zero,
        RequireExpirationTime = true,
        ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha512 }
    };
});
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("user", policy => policy.RequireRole("user"));
    opt.AddPolicy("admin", policy => policy.RequireRole("admin"));
    opt.AddPolicy("all", policy => policy.RequireRole("admin", "user"));
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/login", (UserDTO UserDTO) =>
{
    var token = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
        issuer: Issuer,
        audience: Audience,
        claims: new[] {
               new Claim(ClaimTypes.Role, UserDTO.Role),
               new Claim(ClaimTypes.Name,UserDTO.UserName)
        },
        notBefore: DateTime.UtcNow,
        expires: DateTime.UtcNow.AddSeconds(Expires),
        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecurityKey)), SecurityAlgorithms.HmacSha512)
        ));
    return new { result = true, message = "", data = token };

}).AllowAnonymous();

app.MapGet("/admin", (HttpContext context) =>
{
    return new { result = true, message = "用户名是：" + context.User.Identity.Name, data = "admin" };
}).RequireAuthorization("admin");

app.MapGet("/user", (HttpContext context) =>
{
    return new { result = true, message = "用户名是：" + context.User.Identity.Name, data = "user" };
}).RequireAuthorization("user");

app.MapGet("/all", (HttpContext context) =>
{
    return new { result = true, message = "用户名是：" + context.User.Identity.Name, data = "all" };
}).RequireAuthorization("all");

app.Run();

class UserDTO
{
    public string UserName { get; set; }
    public string Role { get; set; }
}
/*
   "AuthConfig": {
    "Issuer": "https://www.just-agi.com/tran",
    "Audience": "https://www.just-agi.com/tran",
    "Expires": 86400,
    "SecurityKey": "1234567890plmnbvc1234567890qwertyu1234567890abcdefg1234567890abcdefg"
  },
 
 */