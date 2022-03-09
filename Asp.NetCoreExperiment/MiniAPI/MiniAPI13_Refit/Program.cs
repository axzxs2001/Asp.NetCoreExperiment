using Refit;

var builder = WebApplication.CreateBuilder(args);
//配置RefitClient
builder.Services
    .AddRefitClient<IUserAPI>()
    .ConfigureHttpClient(httpclient => httpclient.BaseAddress = new Uri("http://localhost:5026"));

var app = builder.Build();

#region 调用者
app.MapGet("/user", async (ILogger<Program> Logger, IUserAPI userAPI) =>
 {
     Logger.LogInformation("调用者 get user");
     var user = await userAPI.GetUser("gsw");
     user.Name += "丰";
     return user;
 });
app.MapPost("/user", async (ILogger<Program> Logger, IUserAPI userAPI) =>
{
    Logger.LogInformation("调用者 add user");
    var user = new User { UserName = "ls", Name = "李四", Password = "EDCBA", CreateTime = DateTime.Now };
    var newUser = await userAPI.AddUser(user);
    return newUser;
});
app.MapPut("/user", async (ILogger<Program> Logger, IUserAPI userAPI) =>
{
    Logger.LogInformation("调用者 modify user");
    var user = new User { ID = 2, UserName = "ls", Name = "李四收", Password = "AAAAA" };
    return await userAPI.ModifyUser(user);
});
app.MapDelete("/user", async (ILogger<Program> Logger, IUserAPI userAPI) =>
{
    Logger.LogInformation("调用者 remove user");
    return await userAPI.RemoveUser(2);
});

#endregion


#region 被调用API
app.MapGet("/users/{username}", (ILogger<Program> Logger, string userName) =>
{
    Logger.LogInformation("被调用 get user");
    return DB.users.SingleOrDefault(s => s.UserName == userName);
});

app.MapPost("/user", (ILogger<Program> Logger, User user) =>
{
    Logger.LogInformation("被调用 add user");
    user.ID = DB.users.Count + 1;
    DB.users.Add(user);
    return user;
});
app.MapPut("/user", (ILogger<Program> Logger, User user) =>
{
    Logger.LogInformation("被调用 modify user");
    var oldUser = DB.users.SingleOrDefault(s => s.ID == user.ID);
    if (oldUser != null)
    {
        oldUser.UserName = user.UserName;
        oldUser.Password = user.Password;
        oldUser.Name = user.Name;
        oldUser.ModifyTime = DateTime.Now;
    }
    return oldUser;
});
app.MapDelete("/user/{id}", (ILogger<Program> Logger, int id) =>
{
    Logger.LogInformation("被调用 remove user");
    var oldUser = DB.users.SingleOrDefault(s => s.ID == id);
    if (oldUser != null)
    {
        return DB.users.Remove(oldUser);
    }
    else
    {
        return false;
    }
});
#endregion
app.Run();




/// <summary>
/// 定义Refit接口
/// </summary>
public interface IUserAPI
{
    [Get("/users/{username}")]
    Task<User> GetUser(string userName);

    [Post("/user")]
    Task<User> AddUser(User user);

    [Put("/user")]
    Task<User> ModifyUser(User user);

    [Delete("/user/{id}")]
    Task<bool> RemoveUser(int id);
}


#region 存储和实体类
/// <summary>
/// 假装数据库
/// </summary>
public static class DB
{
    /// <summary>
    /// 假装数据表
    /// </summary>
    public static List<User> users = new List<User>() {
    new User{ID=1,UserName="gsw",Name="张三",Password="ABCDE",CreateTime=DateTime.Now}
    };
}
/// <summary>
/// 实体类
/// </summary>
public class User
{
    public int ID { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime ModifyTime { get; set; }
}
#endregion