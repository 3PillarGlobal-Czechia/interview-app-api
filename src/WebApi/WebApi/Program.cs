using Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using WebApi.Modules;

namespace WebApi;

public static class Program
{
    public static int Main(string[] args)
    {
        string websiteName = Environment.MachineName;
        string appName = $"{nameof(WebApi)}";
        try
        {
            // Console logging for used for troubleshooting before we get all needed info for logger
            Logging.LoggerConfigurationExtensions.SetupLoggerConfiguration(appName,websiteName);
            
            appName = ConfigurationExtension.GetConfigurationValue("APP_NAME", "WebApi");
            websiteName = ConfigurationExtension.GetConfigurationValue("WEBSITE_SITE_NAME", Environment.MachineName);
            Logging.LoggerConfigurationExtensions.SetupLoggerConfiguration(appName,websiteName);
            
            Log.Information("Starting web host App: {AppName} WebsiteName: {WebsiteName}", appName,websiteName);
            CreateHostBuilder(args,appName,websiteName).Build().Run();
            Log.Information("Ending web host App: {AppName} WebsiteName: {WebsiteName}", appName,websiteName);
            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly App: {AppName} WebsiteName: {WebsiteName}", appName,websiteName);
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static IHostBuilder CreateHostBuilder(string[] args,string appName, string websiteName) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog((hostBuilderContext, services, loggerConfiguration) =>
            {
                loggerConfiguration.ConfigureBaseLogging(appName, websiteName);
                loggerConfiguration.AddApplicationInsightsLogging(services, hostBuilderContext.Configuration);
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}