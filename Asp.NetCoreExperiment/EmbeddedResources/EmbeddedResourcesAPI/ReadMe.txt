
步骤：
1、把view.cshtml和*.js,*.css属性改成“嵌入的资源”
2、在view中引用js或css样式时，加上wwwroot
3、给路由标志  HttpGet,HttpPost,HttpPut,HttpDelete谓词
4、在使用资源项目的Starup.cs文件中添加如下代码：
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation(option =>
            {
                option.FileProviders.Add(new EmbeddedFileProvider(typeof(EmbeddedResourcesPage.Controllers.HomeController).GetTypeInfo().Assembly));
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        { 
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new EmbeddedFileProvider(typeof(EmbeddedResourcesPage.Controllers.HomeController).GetTypeInfo().Assembly)
            }); 
			……
        }