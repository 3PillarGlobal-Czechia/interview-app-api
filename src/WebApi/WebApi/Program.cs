using Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using WebApi.Utils;

namespace WebApi;

public static class Program
{
    private static string _websiteName = string.Empty;
    private static string _appName = string.Empty;
    public static int Main(string[] args)
    {
        try
        {
            _appName = ConfigurationExtension.GetConfigurationValue("APP_NAME", "WebApi");
            _websiteName = ConfigurationExtension.GetConfigurationValue("WEBSITE_SITE_NAME", Environment.MachineName);
            
            Logging.LoggerConfigurationExtensions.SetupLoggerConfiguration(_appName,_websiteName);
            
            Log.Information("Starting web host App: {AppName} WebsiteName: {WebsiteName}", _appName,_websiteName);
            CreateHostBuilder(args,_appName,_websiteName).Build().Run();
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

    private static IHostBuilder CreateHostBuilder(string[] args,string appName, string websiteName) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog((hostBuilderContext, services, loggerConfiguration) =>
            {
                loggerConfiguration.ConfigureBaseLogging(appName, websiteName);
                loggerConfiguration.WriteTo.Console();
                loggerConfiguration.AddApplicationInsightsLogging(services, hostBuilderContext.Configuration);
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}