﻿using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace Logging
{
    public static class LoggerConfigurationExtensions
    {
        public static void SetupLoggerConfiguration(string appName, string webSiteName)
        {
            Log.Logger = new LoggerConfiguration()
                .ConfigureBaseLogging(appName, webSiteName)
                .CreateLogger();
        }

        public static LoggerConfiguration ConfigureBaseLogging(
            this LoggerConfiguration loggerConfiguration,
            string appName, string webSiteName
        )
        {
            loggerConfiguration
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .WriteTo.Async(a => a.Console(theme: AnsiConsoleTheme.Code))
                .Enrich.FromLogContext()
                .Enrich.WithCorrelationId()
                .Enrich.WithCorrelationIdHeader()
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentName()
                .Enrich.WithEnvironmentUserName()
                .Enrich.WithAssemblyInformationalVersion()
                .Enrich.WithMemoryUsage()
                .Enrich.WithProperty("ApplicationName", appName)
                .Enrich.WithProperty("Env", webSiteName);

            return loggerConfiguration;
        }

        public static LoggerConfiguration AddApplicationInsightsLogging(this LoggerConfiguration loggerConfiguration, IServiceProvider services, IConfiguration configuration)
        {
            string instrumentationKey = configuration.GetValue<string>("ApplicationInsights:InstrumentationKey");
            if (!string.IsNullOrWhiteSpace(instrumentationKey))
            {
                loggerConfiguration.WriteTo.ApplicationInsights(
                    services.GetRequiredService<TelemetryConfiguration>(),
                    TelemetryConverter.Traces);
            }
            return loggerConfiguration;
        }
    }
}