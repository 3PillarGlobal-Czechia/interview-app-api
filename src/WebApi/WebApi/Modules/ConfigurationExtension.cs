using System;
using Serilog;

namespace WebApi.Modules;

public static class ConfigurationExtension
{
    public static T GetConfigurationValue<T>(string key , T defaultValue)
    {
        Log.Information("GetConfigurationValue => Getting value for {Key}",key);
       
        string value = Environment.GetEnvironmentVariable(key);
        if (string.IsNullOrWhiteSpace(value))
        {
            Log.Information("GetConfigurationValue => Getting default value for {Key} => {Value}",key,defaultValue);
            return defaultValue;
        }

        T res = (T) Convert.ChangeType(value, typeof(T));
        Log.Information("GetConfigurationValue => Getting default value for {Key} => {Value}",key,res);
        return res;
    }
}