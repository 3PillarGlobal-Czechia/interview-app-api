using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Serilog;
using Serilog.Context;

namespace Logging;

public class TimeElapsedDiagnosticsMiddleware
{
    private readonly RequestDelegate _next;
    private const string CorrelationId = "correlation-id";

    public TimeElapsedDiagnosticsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        Stopwatch sw = Stopwatch.StartNew();
        string correlationId = GetCorrelationId(context);
        SetCorrelationId(correlationId,context);
        
        using (LogContext.PushProperty("CorrelationId", correlationId))
        {
            try
            {
                await _next.Invoke(context);
                PushPropertyElapsed(sw);
                Log.Information("TimeElapsedDiagnosticsMiddleware - OK - CorrelationId: {CorrelationId}",correlationId);
            }
            catch (Exception e)
            {
                PushPropertyElapsed(sw);
                Log.Fatal(e,"TimeElapsedDiagnosticsMiddleware - ERROR - CorrelationId: {CorrelationId} Message: {Message}", correlationId, e.Message);
            }
        }
    }

    private static string GetCorrelationId(HttpContext context)
    {
        context.Request.Headers.TryGetValue("correlation-id", out StringValues correlationIds);
        return correlationIds.FirstOrDefault() ?? Guid.NewGuid().ToString();
    }
    
    private static void SetCorrelationId(string correlationId, HttpContext context)
    {
        context.Response.Headers.TryAdd($"x-{CorrelationId}",correlationId);
    }

    private static void PushPropertyElapsed(Stopwatch sw)
    {
        sw.Stop();
        LogContext.PushProperty("Elapsed", sw.ElapsedMilliseconds);
    }
}