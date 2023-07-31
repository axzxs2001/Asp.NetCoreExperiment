using AutoUpdateService;
using Quartz;

var builder = WebApplication.CreateSlimBuilder(args);
var upgradeSetting = new UpgradeSettingModel();
builder.Configuration.Bind("UpgradeSetting", upgradeSetting);
if (upgradeSetting.Enable && !string.IsNullOrWhiteSpace(upgradeSetting.CronExpression) && !string.IsNullOrWhiteSpace(upgradeSetting.UpgradeServerUrl))
{
    builder.Services.AddSingleton(upgradeSetting);
    builder.Services.AddHttpClient("upgrade", c =>
    {
        c.BaseAddress = new Uri(upgradeSetting.UpgradeServerUrl);
    });
    builder.Services.AddQuartz(q =>
    {
        q.UseMicrosoftDependencyInjectionJobFactory();

        var jobKey = new JobKey("UpgradeJob");
        q.AddJob<UpgradJob>(opts => opts.WithIdentity(jobKey));

        q.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity("UpgradJob-trigger")
            .WithCronSchedule(upgradeSetting.CronExpression)
        );
    });
    builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
}


var app = builder.Build();
app.UseStaticFiles();


app.Run();

