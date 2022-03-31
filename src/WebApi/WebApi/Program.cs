using Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using WebApi.Modules;

namespace WebApi;

public static class Program
{
    private static string _websiteName = Environment.MachineName;
    private static string _appName = $"{nameof(WebApi)}";
    public static int Main(string[] args)
    {
        try
        {
            // Console logging for used for troubleshooting before we get all needed info for logger
            Logging.LoggerConfigurationExtensions.SetupLoggerConfiguration(_appName,_websiteName);
            
            _appName = ConfigurationExtension.GetConfigurationValue("APP_NAME", "WebApi");
            _websiteName = ConfigurationExtension.GetConfigurationValue("WEBSITE_SITE_NAME", Environment.MachineName);
            Logging.LoggerConfigurationExtensions.SetupLoggerConfiguration(_appName,_websiteName);
            
            Log.Information("Starting web host App: {AppName} WebsiteName: {WebsiteName}", _appName,_websiteName);
            CreateHostBuilder(args).Build().Run();
            Log.Information("Ending web host App: {AppName} WebsiteName: {WebsiteName}", _appName,_websiteName);
            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly App: {AppName} WebsiteName: {WebsiteName}", _appName,_websiteName);
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog((hostBuilderContext, services, loggerConfiguration) =>
            {
                loggerConfiguration.ConfigureBaseLogging(_appName, _websiteName);
                loggerConfiguration.AddApplicationInsightsLogging(services, hostBuilderContext.Configuration);
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}