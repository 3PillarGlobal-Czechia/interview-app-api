using Microsoft.Extensions.Configuration;

namespace WebApi.Modules
{
    public class ConfigurationProvider : Application.Providers.IConfigurationProvider
    {
        private readonly IConfiguration Configuration;

        public ConfigurationProvider(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string GetConfiguration(string key)
        {
            return Configuration.GetValue<string>(key);
        }
    }
}
