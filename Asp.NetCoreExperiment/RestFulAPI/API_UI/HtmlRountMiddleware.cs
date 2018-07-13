using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API_UI
{
    public class HtmlRountMiddleware
    {
        private readonly RequestDelegate _next;

        static List<string> _files;
        public HtmlRountMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public Task InvokeAsync(HttpContext context)
        {
            var dirpath = Directory.GetCurrentDirectory() + "/wwwroot";
            if (_files == null)
            {
                _files = GetFiles(dirpath);           
            }
            if (_files.Contains(context.Request.Path.Value.ToLower()) && context.Request.Path.HasValue)
            {
                context.Request.Path = new PathString(context.Request.Path.Value + ".html");
                if (context.Request.Path == "/login.html")
                {
                    _next(context);
                }
                else
                {
                    if (context.Response.Body.CanWrite)
                    {
                        var index = File.ReadAllText(Directory.GetCurrentDirectory() + "/wwwroot/main/laout.html");
                        var aaa = File.ReadAllText(Directory.GetCurrentDirectory() + "/wwwroot/" + context.Request.Path.Value);
                        var all = index.Replace("#body", aaa);
                        context.Response.WriteAsync(all);
                    }
                }
            }
            else
            {
                _next(context);
            }
            return Task.CompletedTask;
        }
        List<string> GetFiles(string path, string basePath = null)
        {
            if (string.IsNullOrEmpty(basePath))
            {
                basePath = path;
            }
            var files = new List<string>();
            foreach (var file in Directory.GetFiles(path))
            {
                if (Path.GetExtension(file).ToLower() == ".html")
                {
                    files.Add(file.Replace(basePath, "").Replace(Path.GetExtension(file), "").Replace("\\", "/"));
                }
            }
            foreach (var dir in Directory.GetDirectories(path))
            {
                files.AddRange(GetFiles(dir, basePath));
            }
            return files;
        }
    }

    public static class HtmlRountExtension
    {
        public static IApplicationBuilder UserHtmlRount(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HtmlRountMiddleware>();
        }
    }
}
