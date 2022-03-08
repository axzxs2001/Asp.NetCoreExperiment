var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IGoogleService, GoogleService>();
builder.Services.AddHttpClient<IBaiDuService, BaiDuService>(httpClient =>
{
    httpClient.BaseAddress = new Uri("https://www.baidu.com/");
});

var app = builder.Build();

app.MapGet("/testgoogle", async (IGoogleService google) =>
 {
     return await google.GetContentAsync();
 });
app.MapGet("/testbaidu", async (IBaiDuService baidu) =>
{
    return await baidu.GetContentAsync();
});
app.Run();

interface IGoogleService
{
    Task<string> GetContentAsync();
}
class GoogleService : IGoogleService
{
    private readonly HttpClient _httpClient;
    public GoogleService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://www.google.com/");

    }
    public async Task<string> GetContentAsync()
    {
        return await _httpClient.GetStringAsync("search?q=¹ðËØÎ°");
    }
}
interface IBaiDuService
{
    Task<string> GetContentAsync();
}
class BaiDuService : IBaiDuService
{
    private readonly HttpClient _httpClient;
    public BaiDuService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<string> GetContentAsync()
    {
        return await _httpClient.GetStringAsync("s?wd=¹ðËØÎ°");
    }
}