using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Context;

namespace Logging;

public class TimeElapsedDiagnosticsMiddleware
{
    private readonly RequestDelegate _next;

    public TimeElapsedDiagnosticsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        Stopwatch sw = Stopwatch.StartNew();
        try
        {
            await _next(context);
            PushPropertyElapsed(sw);
            Log.Debug("DiagnosticsMiddleware:OK");
        }
        catch (Exception e)
        {
            PushPropertyElapsed(sw);
            Log.Fatal("DiagnosticsMiddleware:ERROR Message: {message} , Exception: {exception}", e.Message,e);
        }
    }

    private static void PushPropertyElapsed(Stopwatch sw)
    {
        sw.Stop();
        LogContext.PushProperty("Elapsed", sw.ElapsedMilliseconds);
    }
}