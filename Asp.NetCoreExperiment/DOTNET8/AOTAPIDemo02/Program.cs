var builder = WebApplication.CreateBuilder(args);



var app = builder.Build();



app.MapGet("/test", () => {
    return "ok";
});


app.MapGet("/test/{id:int}", (int id) => { return $"��ã�{id}"; });

app.MapGet("/orders/{tel:regex(^\\d{{3,4}}(-\\d{{4}}){{2}}$)}", (string tel) => { return $"�绰��{tel}"; });

app.Run();

