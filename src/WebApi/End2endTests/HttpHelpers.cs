using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;

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

    public static HttpContent CreateBodyContent<T>(T payload) => new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
}
