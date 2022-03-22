using System;
using System.Net;
using System.Threading.Tasks;
using WebApi;
using WebApi.UseCases.v1.QuestionSet.UpdateQuestionSet;
using Xunit;

namespace End2EndTests.QuestionListTests;

public class UpdateQuestionSetTests : E2ETestsBase, IClassFixture<MyWebApplicationFactory<Startup>>
{
    private readonly Func<int, string> _url = (int id) => $"/api/v1/QuestionSet/{id}";

    public UpdateQuestionSetTests(MyWebApplicationFactory<Startup> factory) : base(factory)
    {
    }

    private async Task<StandardResponse> EndpointCall(int id, UpdateQuestionSetRequest request) => await _wrapper.PutAsync(_url(id), request);

    [Fact]
    public async Task Update_ValidRequest_ReturnsOk()
    {
        var request = new UpdateQuestionSetRequest
        {
            Title = "test"
        };

        var result = await EndpointCall(1, request);

        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task Update_NonExistingRequest_ReturnsNotFound()
    {
        var request = new UpdateQuestionSetRequest
        {
            Title = "test"
        };

        var result = await EndpointCall(0, request);

        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task Update_InvalidRequest_ReturnsBadRequest()
    {
        var request = new UpdateQuestionSetRequest
        {
            QuestionsToAdd = new int[] { 0 }
        };

        var result = await EndpointCall(1, request);

        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }
}
