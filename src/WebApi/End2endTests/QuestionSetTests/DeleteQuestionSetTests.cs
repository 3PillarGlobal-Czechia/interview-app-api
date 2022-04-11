using Domain.Models;
using System;
using System.Net;
using System.Threading.Tasks;
using WebApi;
using Xunit;

namespace End2EndTests.QuestionSetTests;

public class DeleteQuestionSetTests : E2ETestsBase, IClassFixture<MyWebApplicationFactory<Startup>>
{
    private readonly Func<int, string> _url = (int id) => $"/api/v1/QuestionSet/{id}";

    public DeleteQuestionSetTests(MyWebApplicationFactory<Startup> factory) : base(factory)
    {
    }

    private async Task<StandardResponse> EndpointCall(int id) => await _wrapper.DeleteAsync<QuestionSetModel>(_url(id));

    [Fact]
    public async Task Delete_ValidRequest_ReturnsNoContent()
    {
        var result = await EndpointCall(1);

        Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
    }

    [Fact]
    public async Task Delete_InvalidRequest_ReturnsNotFound()
    {          
        var result = await EndpointCall(0);

        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}
