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

    public async Task<StandardResponse<TResponse>> GetAsync<TResponse>(string url, NameValueCollection queryParams = null)
    {
        string getUrl = HttpHelpers.CreateQueryString(url, queryParams);
        HttpResponseMessage response = await _client.GetAsync(getUrl);
        return new StandardResponse<TResponse>(response);
    }

    public async Task<StandardResponse> PostAsync<TRequest>(string url, TRequest payload)
    {
        using var json = HttpHelpers.CreateBodyContent(payload);
        HttpResponseMessage response = await _client.PostAsync(url, json);
        return new StandardResponse(response);
    }

    public async Task<StandardResponse<TResponse>> PostAsync<TRequest, TResponse>(string url, TRequest payload)
    {
        using var json = HttpHelpers.CreateBodyContent(payload);
        HttpResponseMessage response = await _client.PostAsync(url, json);
        return new StandardResponse<TResponse>(response);
    }

    public async Task<StandardResponse> PutAsync<TRequest>(string url, TRequest payload)
    {
        using var json = HttpHelpers.CreateBodyContent(payload);
        HttpResponseMessage response = await _client.PutAsync(url, json);
        return new StandardResponse(response);
    }

    public async Task<StandardResponse<TResponse>> PutAsync<TRequest, TResponse>(string url, TRequest payload)
    {
        using var json = HttpHelpers.CreateBodyContent(payload);
        HttpResponseMessage response = await _client.PutAsync(url, json);
        return new StandardResponse<TResponse>(response);
    }

    public async Task<StandardResponse> DeleteAsync(string url, NameValueCollection queryParams = null)
    {
        string getUrl = HttpHelpers.CreateQueryString(url, queryParams);
        HttpResponseMessage response = await _client.DeleteAsync(getUrl);
        return new StandardResponse(response);
    }
}

