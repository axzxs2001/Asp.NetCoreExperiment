using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Prometheus;
using PrometheusSample.Middlewares;
using PrometheusSample.Services;

using System.Collections.Generic;

namespace PrometheusSample
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
            MetricsHandle(services);
            services.AddScoped<IOrderService, OrderService>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PrometheusSample", Version = "v1" });
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PrometheusSample v1"));
            }

            app.UseRouting();
            //http请求的中间件
            app.UseHttpMetrics();
            app.UseAuthorization();

            //自定义业务跟踪
            app.UseBusinessMetrics();

            app.UseEndpoints(endpoints =>
            {
                //映射监控地址为  /metrics
                endpoints.MapMetrics();
                endpoints.MapControllers();
            });
        }
        /// <summary>
        /// 处理监控事项
        /// </summary>
        /// <param name="services"></param>
        void MetricsHandle(IServiceCollection services)
        {
            var metricsHub = new MetricsHub();
            //counter
            metricsHub.AddCounter("/register", Metrics.CreateCounter("business_register_user", "注册用户数。"));
            metricsHub.AddCounter("/order", Metrics.CreateCounter("business_order_total", "下单总数。"));
            metricsHub.AddCounter("/pay", Metrics.CreateCounter("business_pay_total", "支付总数。"));
            metricsHub.AddCounter("/ship", Metrics.CreateCounter("business_ship_total", "发货总数。"));

            //gauge
            var orderGauge = Metrics.CreateGauge("business_order_count", "当前下单数量。");
            var payGauge = Metrics.CreateGauge("business_pay_count", "当前支付数量。");
            var shipGauge = Metrics.CreateGauge("business_ship_count", "当前发货数据。");

            metricsHub.AddGauge("/order", new Dictionary<string, Gauge> {
                { "+", orderGauge}
            });
            metricsHub.AddGauge("/pay", new Dictionary<string, Gauge> {
                {"-",orderGauge},
                {"+",payGauge}
            });
            metricsHub.AddGauge("/ship", new Dictionary<string, Gauge> {
                {"+",shipGauge},
                {"-",payGauge}
            });
            //summary 百分位数[在一组由小到大的数字中，某个数字大于80%的数字，这个数字就第80个的百分位数]
            /*0.5-quantile后面是0.05，0.9-quantile后面是0.01，而0.95后面是0.005，而0.99后面是0.001。这些是我们设置的能容忍的误差。0.5-quantile: 0.05意思是允许最后的误差不超过0.05。假设某个0.5-quantile的值为120，由于设置的误差为0.05，所以120代表的真实quantile是(0.45, 0.55)范围内的某个值。
             */
            var orderSummary = Metrics
     .CreateSummary("business_order_summary", "10分钟内的订单数量",
         new SummaryConfiguration
         {
             Objectives = new[]
             {
                new QuantileEpsilonPair(0.1, 0.05),   
                new QuantileEpsilonPair(0.3, 0.05),      
                new QuantileEpsilonPair(0.5, 0.05),
                new QuantileEpsilonPair(0.7, 0.05),           
                new QuantileEpsilonPair(0.9, 0.05),
             }
         });
            metricsHub.AddSummary("/order", orderSummary);

            //histogram
            /*
             grafana中  histogram_quantile(0.95, rate(business_order_histogram_seconds_bucket[5h]))
             95%的订单金额小于等于这个值
             */

            var orderHistogram = Metrics.CreateHistogram("business_order_histogram", "订单直方图。",
        new HistogramConfiguration
        {
             //Buckets = Histogram.ExponentialBuckets(start: 1000, factor: 2, count: 5)
           Buckets = Histogram.LinearBuckets(start: 1000, width: 1000, count: 6)
        }) ;
         
            metricsHub.AddHistogram("/order", orderHistogram);



            services.AddSingleton(metricsHub);
        }
    }
}
