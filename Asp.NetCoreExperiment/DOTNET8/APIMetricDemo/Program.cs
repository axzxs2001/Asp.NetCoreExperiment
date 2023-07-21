using APIMetricDemo;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using System.Diagnostics.Metrics;
using OpenTelemetry.Exporter;
using OpenTelemetry.Instrumentation.AspNetCore;
using OpenTelemetry.Instrumentation.Http;
using OpenTelemetry.AutoInstrumentation.Instrumentations;
using OpenTelemetry.Logs;
using OpenTelemetry.Trace;


var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddOpenTelemetry().WithMetrics(builder =>
{
    builder.AddAspNetCoreInstrumentation();
    builder.AddPrometheusExporter();
    builder.AddHttpClientInstrumentation();
    builder.AddProcessInstrumentation();
    builder.AddRuntimeInstrumentation();
    builder.AddPrometheusHttpListener();

    builder.AddMeter("Microsoft.AspNetCore.Hosting", "Microsoft.AspNetCore.Server.Kestrel", "Custome");
    builder.AddView("http-server-request-duration", new ExplicitBucketHistogramConfiguration
    {
        Boundaries = new double[] { 0, 0.005, 0.01, 0.025, 0.05,
                       0.075, 0.1, 0.25, 0.5, 0.75, 1, 2.5, 5, 7.5, 10 }
    });
});
var app = builder.Build();

app.MapPrometheusScrapingEndpoint();


var sampleTodos = TodoGenerator.GenerateTodos().ToArray();
var todosApi = app.MapGroup("/todos");
todosApi.MapGet("/", () =>
{
    DiagnosticsConfig.RequestCounter.Add(1,
        new("Path", "/todos"),
        new("Method", "get"));
    return sampleTodos;
});
todosApi.MapGet("/{id}", (int id) =>
    sampleTodos.FirstOrDefault(a => a.Id == id) is { } todo
        ? Results.Ok(todo)
        : Results.NotFound());

app.Run();

public static class DiagnosticsConfig
{
    public const string ServiceName = "Custome";
    public static Meter Meter = new(ServiceName);
    public static Counter<long> RequestCounter =
        Meter.CreateCounter<long>("getTodos_count");
}