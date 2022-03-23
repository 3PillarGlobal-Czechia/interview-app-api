using System;

namespace WebApi.Utils;

public static class ConfigurationExtension
{
    public static T GetConfigurationValue<T>(string key , T defaultValue)
    {
        string value = Environment.GetEnvironmentVariable(key);
        if (string.IsNullOrWhiteSpace(value))
        {
            return defaultValue;
        }

        return (T)Convert.ChangeType(value, typeof(T));
    }
}