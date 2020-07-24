using App.Metrics;
using App.Metrics.Health;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AppMetricsDemo02
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            #region Metrics监控配置
            string IsOpen = Configuration.GetSection("InfluxDB:IsOpen").Value.ToLower();
            if (IsOpen == "true")
            {
                string database = Configuration.GetSection("InfluxDB")["DataBaseName"];
                string InfluxDBConStr = Configuration.GetSection("InfluxDB")["ConnectionString"];
                string app = Configuration.GetSection("InfluxDB")["app"];
                string env = Configuration.GetSection("InfluxDB")["env"];
                string username = Configuration.GetSection("InfluxDB")["username"];
                string password = Configuration.GetSection("InfluxDB")["password"];
                var uri = new Uri(InfluxDBConStr);

                var metrics = AppMetrics.CreateDefaultBuilder()
                .Configuration.Configure(
                options =>
                {
                    options.AddAppTag(app);
                    options.AddEnvTag(env);
                })
                .Report.ToInfluxDb(
                options =>
                {
                    options.InfluxDb.BaseUri = uri;
                    options.InfluxDb.Database = database;
                    options.InfluxDb.UserName = username;
                    options.InfluxDb.Password = password;
                    options.InfluxDb.CreateDataBaseIfNotExists = true;
                    options.HttpPolicy.BackoffPeriod = TimeSpan.FromSeconds(30);
                    options.HttpPolicy.FailuresBeforeBackoff = 5;
                    options.HttpPolicy.Timeout = TimeSpan.FromSeconds(10);
                    options.FlushInterval = TimeSpan.FromSeconds(5);
                })
                .Build();


                services.AddMetrics(metrics);
                services.AddMetricsReportingHostedService();
                services.AddMetricsTrackingMiddleware();
                services.AddMetricsEndpoints();

                var hmetrics = AppMetricsHealth.CreateDefaultBuilder().Report.ToMetrics(metrics)
                    //.HealthChecks.RegisterFromAssembly(services)    //自定义注册
                    .HealthChecks.AddCheck(new MyHealthCheck("自定义类现"))
                    .HealthChecks.AddCheck("委括实现", () =>
                    {

                        if (DateTime.Now.Second % 3 != 0)
                        {
                            return new ValueTask<HealthCheckResult>(HealthCheckResult.Healthy("Ok"));
                        }
                        else
                        {
                            return new ValueTask<HealthCheckResult>(HealthCheckResult.Unhealthy("error"));
                        }
                    })
                    .BuildAndAddTo(services);
                services.AddHealth(hmetrics);
                services.AddHealthReportingHostedService();
                services.AddHealthEndpoints();


            }

            #endregion

            services.AddMvc().AddMetrics().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            #region 使用中间件Metrics
            string IsOpen = Configuration.GetSection("InfluxDB")["IsOpen"].ToLower();
            if (IsOpen == "true")
            {
                app.UseMetricsAllMiddleware();
                app.UseMetricsAllEndpoints(); ;
                app.UseHealthAllEndpoints();

            }
            #endregion

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
    public class MyHealthCheck : HealthCheck
    {
        public MyHealthCheck(string name = "MyHealthCheck") : base(name)
        {
        }
        protected override ValueTask<HealthCheckResult> CheckAsync(CancellationToken cancellationToken)
        {            
            if (DateTime.Now.Second % 3 != 0)
            {
                return new ValueTask<HealthCheckResult>(HealthCheckResult.Healthy("MyHealthCheck Ok"));
            }
            else
            {
                return new ValueTask<HealthCheckResult>(HealthCheckResult.Unhealthy("MyHealthCheck error"));
            }
        }
    }
}
