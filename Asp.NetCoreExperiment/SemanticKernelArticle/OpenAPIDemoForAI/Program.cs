using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenAPIDemoForAI;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddAuthentication().AddJwtBearer("Bearer", opt =>
{
    //token验证参数
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("1234567890abcdefg1234567890abcdefg1234567890abcdefg1234567890abcdefg")),
        ValidateIssuer = true,
        ValidIssuer = "https://www.just-agi.com/smartfill",
        ValidateAudience = true,
        ValidAudience = "https://www.just-agi.com/smartfill",
        ClockSkew = TimeSpan.Zero,
        RequireExpirationTime = true,
    };

});

builder.Services
    .AddAuthorization(opt =>
    {
        opt.AddPolicy("permission", policyBuilder =>
        {
            policyBuilder.AddRequirements(new AuthorizationRequirement());
            policyBuilder.RequireClaim(ClaimTypes.Email);
            //policyBuilder.RequireClaim(ClaimTypes.Uri);
        });

    })
    .AddScoped<IAuthorizationHandler, PermissionHandler>(); ;
var app = builder.Build();
app.MapOpenApi();
app.MapGet("/orders", () =>
{
    app.Logger.LogInformation("查询orders");
    var orders = Enumerable.Range(1, 5).Select(index =>
        new Order(Guid.NewGuid().ToString(), $"Product {index}", index, index * 10))
        .ToArray();
    return orders;
})
.WithName("orders").WithDescription("获取订单列表").RequireAuthorization("permission"); ;

app.MapPost("/order", ([FromBody] Order order) =>
{
    app.Logger.LogInformation("添加order,{0}", order);
    return order;
}).WithName("order").WithDescription("添加订单").RequireAuthorization("permission"); ;

app.MapGet(pattern: "/gettoken", (string email) =>
{
    return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
issuer: "https://www.just-agi.com/smartfill",
audience: "https://www.just-agi.com/smartfill",
claims: new Claim[] {
                new Claim(ClaimTypes.Email, email)
},
notBefore: DateTime.UtcNow,
expires: DateTime.UtcNow.AddSeconds(23333333),
signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("1234567890abcdefg1234567890abcdefg1234567890abcdefg1234567890abcdefg")),
    SecurityAlgorithms.HmacSha512)));
}).WithName("token").WithDescription("获取token"); ;

app.MapPost( "/login", (UserModel login) =>
{
    var token = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
issuer: "https://www.just-agi.com/smartfill",
audience: "https://www.just-agi.com/smartfill",
claims: new Claim[] {
                new Claim(ClaimTypes.Email, login.UserName)
},
notBefore: DateTime.UtcNow,
expires: DateTime.UtcNow.AddSeconds(23333333),
signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("1234567890abcdefg1234567890abcdefg1234567890abcdefg1234567890abcdefg")),
    SecurityAlgorithms.HmacSha512)));

    return new { Token = token };
}).WithName("login").WithDescription("获取token"); ;


app.Run();
class UserModel
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
class Order
{
    public Order(string id, string product, int quantity, decimal price)
    {
        Id = id;
        Product = product;
        Quantity = quantity;
        Price = price;
    }
    [JsonPropertyName("编号")]
    public string Id { get; set; }
    [JsonPropertyName("产品名称")]
    public string Product { get; set; }
    [JsonPropertyName("订单数量")]
    public int Quantity { get; set; }
    [JsonPropertyName("订单金额")]
    public decimal Price { get; set; }
    public override string ToString() => JsonSerializer.Serialize(this);
}