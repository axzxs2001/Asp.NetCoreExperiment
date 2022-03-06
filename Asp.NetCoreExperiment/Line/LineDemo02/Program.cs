
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var app = builder.Build();
app.Urls.Add("http://*:6789");
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
