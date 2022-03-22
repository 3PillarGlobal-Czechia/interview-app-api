using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace End2EndTests;

public class StandardResponse
{
    public HttpResponseHeaders Headers { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public string ErrorMessage { get; set; }

    public StandardResponse(HttpResponseMessage response)
    {
        Headers = response.Headers;
        StatusCode = response.StatusCode;
        ErrorMessage = !response.IsSuccessStatusCode ? response.Content.ReadAsStringAsync().Result : string.Empty;
    }
}

public class StandardResponse<T> : StandardResponse
{
    public T Data { get; set; }

    public StandardResponse(HttpResponseMessage response) : base(response)
    {
        Data = response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result) : default;
    }
}
