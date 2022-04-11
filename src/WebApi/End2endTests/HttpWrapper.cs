using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;

namespace End2EndTests;

public class HttpWrapper
{
    private readonly HttpClient _client;

    public HttpWrapper(HttpClient client)
    {
        _client = client;
    }

    public async Task<StandardResponse<T>> GetAsync<T>(string url, NameValueCollection queryParams = null)
    {
        string getUrl = HttpHelpers.CreateQueryString(url, queryParams);
        HttpResponseMessage response = await _client.GetAsync(getUrl);
        return new StandardResponse<T>(response);
    }

    public async Task<StandardResponse> PostAsync<T>(string url, T payload)
    {
        var json = HttpHelpers.CreateBodyContent(payload);
        HttpResponseMessage response = await _client.PostAsync(url, json);
        return new StandardResponse(response);
    }

    public async Task<StandardResponse<U>> PostAsync<T, U>(string url, T payload)
    {
        var json = HttpHelpers.CreateBodyContent(payload);
        HttpResponseMessage response = await _client.PostAsync(url, json);
        return new StandardResponse<U>(response);
    }

    public async Task<StandardResponse> PutAsync<T>(string url, T payload)
    {
        var json = HttpHelpers.CreateBodyContent(payload);
        HttpResponseMessage response = await _client.PutAsync(url, json);
        return new StandardResponse(response);
    }

    public async Task<StandardResponse<U>> PutAsync<T, U>(string url, T payload)
    {
        var json = HttpHelpers.CreateBodyContent(payload);
        HttpResponseMessage response = await _client.PutAsync(url, json);
        return new StandardResponse<U>(response);
    }

    public async Task<StandardResponse> DeleteAsync<T>(string url, NameValueCollection queryParams = null)
    {
        string getUrl = HttpHelpers.CreateQueryString(url, queryParams);
        HttpResponseMessage response = await _client.DeleteAsync(getUrl);
        return new StandardResponse(response);
    }
}

