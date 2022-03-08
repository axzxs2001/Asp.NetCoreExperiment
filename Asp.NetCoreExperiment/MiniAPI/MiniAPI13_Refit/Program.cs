using Refit;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddRefitClient<IUserAPI>()
    .ConfigureHttpClient(httpclient => httpclient.BaseAddress = new Uri("http://localhost:5026"));

var app = builder.Build();

//������
app.MapGet("/getuser", async (ILogger<Program> Logger, IUserAPI userAPI) =>
 {
     Logger.LogInformation("������");
     var user = await userAPI.GetUser("gsw");
     user.Name += "��";
     return user;
 });

//������API
app.MapGet("/users/{username}", (ILogger<Program> Logger, string userName) =>
{
    Logger.LogInformation("������API");
    return new User
    {
        UserName = userName,
        Name = "����",
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