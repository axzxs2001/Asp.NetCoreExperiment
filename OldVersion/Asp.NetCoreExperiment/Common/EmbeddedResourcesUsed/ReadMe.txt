
步骤：
1、把view.cshtml和*.js,*.css属性改成“嵌入的资源”
2、在view中引用js或css样式时，加上wwwroot
3、给路由标志  HttpGet,HttpPost,HttpPut,HttpDelete谓词
3、在使用资源项目的Starup.cs文件中添加如下代码：
    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<RazorViewEngineOptions>(options =>
        {
            options.FileProviders.Add(
                new EmbeddedFileProvider(typeof(EmbeddedResourcesProject.Controllers.HomeController).GetTypeInfo().Assembly));

        });          
        services.AddMvc().AddApplicationPart(typeof(EmbeddedResourcesProject.Controllers.HomeController).GetTypeInfo().Assembly).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
    }


    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new EmbeddedFileProvider(typeof(EmbeddedResourcesProject.Controllers.HomeController).GetTypeInfo().Assembly),

        });
        app.UseMvc();
    }