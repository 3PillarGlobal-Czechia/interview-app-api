using System.Net;
using System.Threading.Tasks;
using WebApi;
using WebApi.UseCases.v1.QuestionList.UpdateQuestionList;
using Xunit;

namespace End2EndTests.QuestionListTests;

public class UpdateQuestionListTests : E2ETestsBase, IClassFixture<MyWebApplicationFactory<Startup>>
{
    private readonly string _url = "/api/v1/QuestionLists/Update";

    public UpdateQuestionListTests(MyWebApplicationFactory<Startup> factory) : base(factory)
    {
    }

    private async Task<StandardResponse> EndpointCall(UpdateQuestionListRequest request) => await PostAsync(_url, request);

    [Fact]
    public async Task Update_ValidRequest_ReturnsOk()
    {
        var request = new UpdateQuestionListRequest
        {
            Id = 1,
            Title = "test"
        };

        var result = await EndpointCall(request);

        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task Update_NonExistingRequest_ReturnsNotFound()
    {
        var request = new UpdateQuestionListRequest
        {
            Id = 0,
            Title = "test"
        };

        var result = await EndpointCall(request);

        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task Update_InvalidRequest_ReturnsBadRequest()
    {
        var request = new UpdateQuestionListRequest
        {
            Id = 1,
            QuestionsToAdd = new int[] { 0 }
        };

        var result = await EndpointCall(request);

        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }
}
