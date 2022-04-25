using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace WinFormsBlazor01.Razors
{
    public class BlazorService
    {
        public static IEventHub CretaeBlazorService<IService, Service, RazorPage>(BlazorWebView blazorWebView, string containerName = "app", string hostPage = "wwwroot\\index.html")
            where IService : class
            where Service : class, IService
            where RazorPage : IComponent
        {
            var services = new ServiceCollection();
            services.AddWindowsFormsBlazorWebView();
            services.AddSingleton<IService, Service>();
            var eventHub = new EventHub();
            services.AddSingleton<IEventHub>(eventHub);
            blazorWebView.HostPage = hostPage;
            blazorWebView.Services = services.BuildServiceProvider();

            blazorWebView.RootComponents.Add<RazorPage>($"#{containerName}");
            return eventHub;
        }
        public static IEventHub CretaeBlazorService<RazorPage>(BlazorWebView blazorWebView, string containerName = "app", string hostPage = "wwwroot\\index.html") where RazorPage : IComponent
        {
            var services = new ServiceCollection();
            services.AddWindowsFormsBlazorWebView();
            var eventHub = new EventHub();
            services.AddSingleton<IEventHub>(eventHub);

            blazorWebView.HostPage = hostPage;
            blazorWebView.Services = services.BuildServiceProvider();

            blazorWebView.RootComponents.Add<RazorPage>($"#{containerName}");
            return eventHub;
        }

        public static IEventHub CretaeBlazorService<RazorPage>(BlazorWebView blazorWebView, string containerName = "app", string hostPage = "wwwroot\\index.html", params Control[] controls) where RazorPage : IComponent
        {
            var services = new ServiceCollection();
            services.AddWindowsFormsBlazorWebView();
            var eventHub = new EventHub();
            services.AddSingleton<IEventHub>(eventHub);

            if (controls != null)
            {
                services.AddSingleton(controls);
            }

            blazorWebView.HostPage = hostPage;
            blazorWebView.Services = services.BuildServiceProvider();

            blazorWebView.RootComponents.Add<RazorPage>($"#{containerName}");
            return eventHub;
        }

    }
}
