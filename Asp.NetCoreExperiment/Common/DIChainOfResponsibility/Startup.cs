using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DIChainOfResponsibility
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
            //职责链依赖注入
            var mailTransfer = new StarPayMailTransfer();
            var httpTransfer = new StarPayFtpTransfer();
            var sftpTransfer = new StarPaySFtpTransfer();
            var endNotice = new EndTransfer();
            mailTransfer.Next(httpTransfer);
            httpTransfer.Next(sftpTransfer);
            sftpTransfer.Next(endNotice);
            var transferParmeter = new TransferParmeter();
      
            services.AddTransient(typeof(StarPayTransfer), (provider) =>
            {
                return mailTransfer;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
