using Domain.Models;
using System.Net;
using System.Threading.Tasks;
using WebApi;
using WebApi.UseCases.v1.QuestionSet.CreateQuestionSet;
using Xunit;

namespace End2EndTests.QuestionListTests;

public class CreateQuestionSetTests : E2ETestsBase, IClassFixture<MyWebApplicationFactory<Startup>>
{
    private readonly string _url = "/api/v1/QuestionSet";

    public CreateQuestionSetTests(MyWebApplicationFactory<Startup> factory) : base(factory)
    {
    }

    private async Task<StandardResponse<QuestionSetModel>> EndpointCall(CreateQuestionSetRequest request) => await wrapper.PostAsync<CreateQuestionSetRequest, QuestionSetModel>(_url, request);

    [Fact]
    public async Task Create_ValidRequest_ReturnsOk()
    {
        var request = new CreateQuestionSetRequest
        {
            Title = "test",
            Description = "test",
            InterviewQuestionIds = new[] { 1, 2 }
        };

        var result = await EndpointCall(request);

        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task Create_InvalidRequest_ReturnsBadRequest()
    {
        var request = new CreateQuestionSetRequest
        {
            Title = "test",
            InterviewQuestionIds = new[] { 0 }
        };

        var result = await EndpointCall(request);

        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }
}
