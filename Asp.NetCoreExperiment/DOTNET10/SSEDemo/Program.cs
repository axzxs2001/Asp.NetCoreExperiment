using System.Diagnostics;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/performance", static (CancellationToken cancellationToken) =>
{
    async IAsyncEnumerable<OSPerformance> GetHeartRate(
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {           
            yield return OSPerformance.Create();
            await Task.Delay(2000, cancellationToken);
        }
    }

    return TypedResults.ServerSentEvents(GetHeartRate(cancellationToken), eventType: "osPerformance");
});
app.Run();

public class OSPerformance
{
    public float CPUUsage { get; set; }
    public float MemAvailable { get; set; }
    public DateTime Timestamp { get; set; }
    private OSPerformance(float cpuUsage, float memAvailable, DateTime timestamp)
    {
        CPUUsage = cpuUsage;
        MemAvailable = memAvailable;
        Timestamp = timestamp;
    }
    public static OSPerformance Create()
    {
        var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        var memCounter = new PerformanceCounter("Memory", "Available MBytes");
        cpuCounter.NextValue();
        float cpuUsage = cpuCounter.NextValue();
        float memAvailable = memCounter.NextValue();
        return new OSPerformance(cpuUsage, memAvailable, DateTime.UtcNow);
    }
}
