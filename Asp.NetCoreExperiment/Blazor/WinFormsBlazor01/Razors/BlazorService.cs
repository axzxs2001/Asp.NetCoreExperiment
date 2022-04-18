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
using WinFormsBlazor01.Services;

namespace WinFormsBlazor01.Razors
{
    public class BlazorService
    {
        public static IEventHub CretaeBlazorService<IService, Service, RazorPage>(BlazorWebView blazorWebView)
            where IService : class
            where Service : class, IService
            where RazorPage : IComponent
        {
            var services = new ServiceCollection();
            services.AddWindowsFormsBlazorWebView();
            services.AddSingleton<IService, Service>();
            var eventHub = new EventHub();
            services.AddSingleton<IEventHub>(eventHub);
            blazorWebView.HostPage = "wwwroot\\index.html";
            blazorWebView.Services = services.BuildServiceProvider();

            blazorWebView.RootComponents.Add<RazorPage>("#app");
            return eventHub;
        }
        public static IEventHub CretaeBlazorService<RazorPage>(BlazorWebView blazorWebView) where RazorPage : IComponent
        {
            var services = new ServiceCollection();
            services.AddWindowsFormsBlazorWebView();   
            var eventHub = new EventHub();
            services.AddSingleton<IEventHub>(eventHub);
            blazorWebView.HostPage = "wwwroot\\index.html";
            blazorWebView.Services = services.BuildServiceProvider();

            blazorWebView.RootComponents.Add<RazorPage>("#app");
            return eventHub;
        }
    }
}
