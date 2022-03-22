using System;
using System.Net;
using System.Threading.Tasks;
using WebApi;
using WebApi.UseCases.v1.Question.UpdateQuestion;
using Xunit;

namespace End2EndTests.InterviewQuestionTests;

public class UpdateQuestionTests : E2ETestsBase, IClassFixture<MyWebApplicationFactory<Startup>>
{
    private readonly Func<int, string> _url = (int id) => $"/api/v1/Question/{id}";

    public UpdateQuestionTests(MyWebApplicationFactory<Startup> factory) : base(factory)
    {
    }

    private async Task<StandardResponse> EndpointCall(int id, UpdateQuestionRequest request) => await PutAsync(_url(id), request);

    [Fact]
    public async Task Update_ValidRequest_ReturnsOk()
    {
        var request = new UpdateQuestionRequest
        {
            Title = "testing"
        };

        var result = await EndpointCall(1, request);

        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task Update_NonExistingRequest_ReturnsNotFound()
    {
        var request = new UpdateQuestionRequest
        {
            Title = "testing"
        };

        var result = await EndpointCall(0, request);

        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task Update_InvalidRequest_ReturnsBadRequest()
    {
        var request = new UpdateQuestionRequest
        {
            Difficulty = 6
        };

        var result = await EndpointCall(1, request);

        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }
}
