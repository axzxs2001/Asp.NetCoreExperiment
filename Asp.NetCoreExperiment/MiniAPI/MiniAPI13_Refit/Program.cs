using Refit;

var builder = WebApplication.CreateBuilder(args);
//����RefitClient
builder.Services
    .AddRefitClient<IUserAPI>()
    .ConfigureHttpClient(httpclient => httpclient.BaseAddress = new Uri("http://localhost:5026"));

var app = builder.Build();

#region ������
app.MapGet("/user", async (ILogger<Program> Logger, IUserAPI userAPI) =>
 {
     Logger.LogInformation("������ get user");
     var user = await userAPI.GetUser("gsw");
     user.Name += "��";
     return user;
 });
app.MapPost("/user", async (ILogger<Program> Logger, IUserAPI userAPI) =>
{
    Logger.LogInformation("������ add user");
    var user = new User { UserName = "ls", Name = "����", Password = "EDCBA", CreateTime = DateTime.Now };
    var newUser = await userAPI.AddUser(user);
    return newUser;
});
app.MapPut("/user", async (ILogger<Program> Logger, IUserAPI userAPI) =>
{
    Logger.LogInformation("������ modify user");
    var user = new User { ID = 2, UserName = "ls", Name = "������", Password = "AAAAA" };
    return await userAPI.ModifyUser(user);
});
app.MapDelete("/user", async (ILogger<Program> Logger, IUserAPI userAPI) =>
{
    Logger.LogInformation("������ remove user");
    return await userAPI.RemoveUser(2);
});

#endregion


#region ������API
app.MapGet("/users/{username}", (ILogger<Program> Logger, string userName) =>
{
    Logger.LogInformation("������ get user");
    return DB.users.SingleOrDefault(s => s.UserName == userName);
});

app.MapPost("/user", (ILogger<Program> Logger, User user) =>
{
    Logger.LogInformation("������ add user");
    user.ID = DB.users.Count + 1;
    DB.users.Add(user);
    return user;
});
app.MapPut("/user", (ILogger<Program> Logger, User user) =>
{
    Logger.LogInformation("������ modify user");
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
    Logger.LogInformation("������ remove user");
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
/// ����Refit�ӿ�
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


#region �洢��ʵ����
/// <summary>
/// ��װ���ݿ�
/// </summary>
public static class DB
{
    /// <summary>
    /// ��װ���ݱ�
    /// </summary>
    public static List<User> users = new List<User>() {
    new User{ID=1,UserName="gsw",Name="����",Password="ABCDE",CreateTime=DateTime.Now}
    };
}
/// <summary>
/// ʵ����
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