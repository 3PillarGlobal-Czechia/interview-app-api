using Domain.Models;
using System;
using System.Net;
using System.Threading.Tasks;
using WebApi;
using Xunit;

namespace End2EndTests.QuestionListTests;

public class GetQuestionSetTests : E2ETestsBase, IClassFixture<MyWebApplicationFactory<Startup>>
{
    private readonly Func<int, string> _url = (int id) => $"/api/v1/QuestionSet/{id}";

    public GetQuestionSetTests(MyWebApplicationFactory<Startup> factory) : base(factory)
    {
    }

    private async Task<StandardResponse<QuestionSetModel>> EndpointCall(int id) => await _wrapper.GetAsync<QuestionSetModel>(_url(id));

    [Fact]
    public async Task Get_ValidRequest_ReturnsSingleList()
    {
        var result = await EndpointCall(1);

        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
    }

    [Fact]
    public async Task Get_NonExistingRequest_ReturnsNotFound()
    {
        var result = await EndpointCall(0);

        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}
