using System.Diagnostics.Metrics;

namespace TravelInspiration.API.Shared.Metrics;

public sealed class HandlerPerformanceMetric
{
    private readonly Counter<long> _miliSecondsElapsed;

    public HandlerPerformanceMetric(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create("TravelInspiration.API");
        _miliSecondsElapsed = meter.CreateCounter<long>("travelinspiration.api.requesthandler.milisecondselapsed");
    }

    public void MiliSecondsElapsed(long miliSecondsElapsed) => 
        _miliSecondsElapsed.Add(miliSecondsElapsed);
}
