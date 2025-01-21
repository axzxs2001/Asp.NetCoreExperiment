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
    //token��֤����
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
    app.Logger.LogInformation("��ѯorders");
    var orders = Enumerable.Range(1, 5).Select(index =>
        new Order(Guid.NewGuid().ToString(), $"Product {index}", index, index * 10))
        .ToArray();
    return orders;
})
.WithName("orders").WithDescription("��ȡ�����б�").RequireAuthorization("permission"); ;

app.MapPost("/order", ([FromBody] Order order) =>
{
    app.Logger.LogInformation("���order,{0}", order);
    return order;
}).WithName("order").WithDescription("��Ӷ���").RequireAuthorization("permission"); ;

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
}).WithName("token").WithDescription("��ȡtoken"); ;

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
}).WithName("login").WithDescription("��ȡtoken"); ;


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
    [JsonPropertyName("���")]
    public string Id { get; set; }
    [JsonPropertyName("��Ʒ����")]
    public string Product { get; set; }
    [JsonPropertyName("��������")]
    public int Quantity { get; set; }
    [JsonPropertyName("�������")]
    public decimal Price { get; set; }
    public override string ToString() => JsonSerializer.Serialize(this);
}