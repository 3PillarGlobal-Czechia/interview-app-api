namespace Application.Providers
{
    public interface IConfigurationProvider
    {
        public string GetConfiguration(string key);
    }
}
