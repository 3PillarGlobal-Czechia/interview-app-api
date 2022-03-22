using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using WebApi;

namespace End2EndTests;

public abstract class E2ETestsBase
{
    private readonly HttpClient _client;

    protected E2ETestsBase(MyWebApplicationFactory<Startup> factory)
    {
        _client = factory.CreateClient();
    }

    protected async Task<StandardResponse<T>> GetAsync<T>(string url, NameValueCollection queryParams = null)
    {
        string getUrl = HttpHelpers.CreateQueryString(url, queryParams);
        HttpResponseMessage response = await _client.GetAsync(getUrl);
        return new StandardResponse<T>(response);
    }

    protected async Task<StandardResponse> PostAsync<T>(string url, T payload)
    {
        var json = HttpHelpers.CreateBodyContent(payload);
        HttpResponseMessage response = await _client.PostAsync(url, json);
        return new StandardResponse(response);
    }

    protected async Task<StandardResponse<U>> PostAsync<T, U>(string url, T payload)
    {
        var json = HttpHelpers.CreateBodyContent(payload);
        HttpResponseMessage response = await _client.PostAsync(url, json);
        return new StandardResponse<U>(response);
    }

    protected async Task<StandardResponse> PutAsync<T>(string url, T payload)
    {
        var json = HttpHelpers.CreateBodyContent(payload);
        HttpResponseMessage response = await _client.PutAsync(url, json);
        return new StandardResponse(response);
    }

    protected async Task<StandardResponse<U>> PutAsync<T, U>(string url, T payload)
    {
        var json = HttpHelpers.CreateBodyContent(payload);
        HttpResponseMessage response = await _client.PutAsync(url, json);
        return new StandardResponse<U>(response);
    }
}
