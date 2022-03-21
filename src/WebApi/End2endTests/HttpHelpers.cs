using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace End2EndTests;

internal static class HttpHelpers
{
    public static string CreateQueryString(string url, NameValueCollection nvc)
    {
        if (nvc == null)
        {
            return url;
        }

        StringBuilder sb = new(url.Contains('?') ? "&" : "?");

        bool first = true;

        foreach (string key in nvc.AllKeys)
        {
            foreach (string value in nvc.GetValues(key) ?? Array.Empty<string>())
            {
                if (!first)
                {
                    sb.Append('&');
                }

                sb.Append($"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(value)}");

                first = false;
            }
        }

        return url + sb.ToString();
    }

    public static HttpContent CreatePostContent<T>(T payload) => new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

    public static StandardResponse CreateStandardResponse(HttpResponseMessage response)
    {
        return new StandardResponse
        {
            Headers = response.Headers,
            StatusCode = response.StatusCode
        };
    }

    public static async Task<StandardResponse<T>> CreateStandardResponse<T>(HttpResponseMessage response)
    {
        try
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            return new StandardResponse<T>
            {
                Headers = response.Headers,
                StatusCode = response.StatusCode,
                Data = !string.IsNullOrWhiteSpace(responseBody) && response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<T>(responseBody) : default,
                ErrorMessage = !response.IsSuccessStatusCode ? responseBody : string.Empty
            };
        }
        catch (JsonException ex)
        {
            throw new Exception("Deserialization exception", ex.InnerException);
        }
    }
}
