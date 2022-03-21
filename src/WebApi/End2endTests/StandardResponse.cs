using System.Net;
using System.Net.Http.Headers;

namespace End2EndTests;

public class StandardResponse
{
    public HttpResponseHeaders Headers { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public string ErrorMessage { get; set; }
}

public class StandardResponse<T> : StandardResponse
{
    public T Data { get; set; }
}
