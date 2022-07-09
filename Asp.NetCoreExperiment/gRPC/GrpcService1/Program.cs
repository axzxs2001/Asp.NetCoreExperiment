using GrpcService1.Services;

namespace GrpcService1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddGrpc();
            var app = builder.Build();          
            app.MapGrpcService<ToDoService>();
            app.Run();
        }
    }
}