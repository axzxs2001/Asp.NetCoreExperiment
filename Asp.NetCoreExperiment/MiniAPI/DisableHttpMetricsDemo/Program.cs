using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.HttpResults;
using OpenTelemetry.Metrics;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecks();
builder.Services.AddOpenTelemetry()
    .WithMetrics(builder =>
    {
        builder.AddMeter("Microsoft.AspNetCore.Hosting",
                         "Microsoft.AspNetCore.Server.Kestrel");
        builder.AddView("http.server.request.duration",
            new ExplicitBucketHistogramConfiguration
            {
                Boundaries = new double[] { 0, 0.005, 0.01, 0.025, 0.05,
                       0.075, 0.1, 0.25, 0.5, 0.75, 1, 2.5, 5, 7.5, 10 }
            });
    });

var app = builder.Build();

app.Use(async (context, next) =>
{
    if (context.Request.Headers.ContainsKey("x-disable-metrics"))
    {
        var feature = context.Features.Get<IHttpMetricsTagsFeature>();
        if (feature != null)
        {
            feature.MetricsDisabled = true;
        }
    }
    await next(context);
});
app.MapGet("/test", () =>
{
    return "ok";
});
app.MapHealthChecks("/healthz").DisableHttpMetrics();
app.Run();

//dotnet-counters monitor -n DisableHttpMetricsDemo --counters Microsoft.AspNetCore.Hosting 