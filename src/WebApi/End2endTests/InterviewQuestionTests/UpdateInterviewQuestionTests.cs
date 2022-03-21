using System.Net;
using System.Threading.Tasks;
using WebApi;
using WebApi.UseCases.v1.UpdateInterviewQuestion;
using Xunit;

namespace End2EndTests.InterviewQuestionTests;

public class UpdateInterviewQuestionTests : E2ETestsBase, IClassFixture<MyWebApplicationFactory<Startup>>
{
    private readonly string _url = "/api/v1/InterviewQuestions/Update";

    public UpdateInterviewQuestionTests(MyWebApplicationFactory<Startup> factory) : base(factory)
    {
    }

    private async Task<StandardResponse> EndpointCall(UpdateInterviewQuestionRequest request) => await PostAsync(_url, request);

    [Fact]
    public async Task Update_ValidRequest_ReturnsOk()
    {
        var request = new UpdateInterviewQuestionRequest
        {
            Id = 1,
            Title = "testing"
        };

        var result = await EndpointCall(request);

        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task Update_NonExistingRequest_ReturnsNotFound()
    {
        var request = new UpdateInterviewQuestionRequest
        {
            Id = 0
        };

        var result = await EndpointCall(request);

        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task Update_InvalidRequest_ReturnsBadRequest()
    {
        var request = new UpdateInterviewQuestionRequest
        {
            Id = 1,
            Difficulty = 6
        };

        var result = await EndpointCall(request);

        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }
}
