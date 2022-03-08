using Refit;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddRefitClient<IUserAPI>()
    .ConfigureHttpClient(httpclient => httpclient.BaseAddress = new Uri("http://localhost:5026"));

var app = builder.Build();

//调用者
app.MapGet("/getuser", async (ILogger<Program> Logger, IUserAPI userAPI) =>
 {
     Logger.LogInformation("调用者");
     var user = await userAPI.GetUser("gsw");
     user.Name += "丰";
     return user;
 });

//被调用API
app.MapGet("/users/{username}", (ILogger<Program> Logger, string userName) =>
{
    Logger.LogInformation("被调用API");
    return new User
    {
        UserName = userName,
        Name = "张三",
        Password = "ABCDE"
    };
});

app.Run();

public interface IUserAPI
{
    [Get("/users/{username}")]
    Task<User> GetUser(string userName);
}

public partial class User
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
}