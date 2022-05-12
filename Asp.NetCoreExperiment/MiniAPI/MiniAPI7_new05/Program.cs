var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();

app.MapGet("/test_ok", () =>
{
    return TypedResults.Ok<Data>(new Data { Id = 1, Name = "测试产品", Price = 10.2m });
});
app.MapGet("/test_json", () =>
{
    return TypedResults.Json<Data>(new Data { Id = 1, Name = "测试产品", Price = 10.2m });
});
app.MapGet("/test_localredirect", () =>
{
    return TypedResults.LocalRedirect("/test_ok");
});
app.MapGet("/test_redirect", () =>
{
    return TypedResults.Redirect("https://www.google.com");
});
app.MapGet("/test_file", () =>
{
    var bytes = "这是一个测试"u8;
    return TypedResults.File(bytes, contentType: "text", fileDownloadName: "test.txt");
});
app.MapGet("/test_physicalfile", () =>
{
    Results
    return TypedResults.PhysicalFile(Directory.GetCurrentDirectory() + "/download.txt", contentType: "text", fileDownloadName: "download.txt");
});
//app.MapGet("/test_virtualfile", () =>
//{
//    return TypedResults.VirtualFile("download.txt", contentType: "text", fileDownloadName: "download.txt");
//});

app.Run();



public class Data
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal? Price { get; set; }
}