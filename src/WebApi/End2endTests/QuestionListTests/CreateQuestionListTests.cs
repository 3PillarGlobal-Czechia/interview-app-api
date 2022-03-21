using Domain.Models;
using System.Net;
using System.Threading.Tasks;
using WebApi;
using WebApi.UseCases.v1.QuestionList.CreateQuestionList;
using Xunit;

namespace End2EndTests.QuestionListTests;

public class CreateQuestionListTests : E2ETestsBase, IClassFixture<MyWebApplicationFactory<Startup>>
{
    private readonly string _url = "/api/v1/QuestionLists/Create";

    public CreateQuestionListTests(MyWebApplicationFactory<Startup> factory) : base(factory)
    {
    }

    private async Task<StandardResponse<QuestionListModel>> EndpointCall(CreateQuestionListRequest request) => await PostAsync<CreateQuestionListRequest, QuestionListModel>(_url, request);

    [Fact]
    public async Task Create_ValidRequest_ReturnsOk()
    {
        var request = new CreateQuestionListRequest
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
        var request = new CreateQuestionListRequest
        {
            Title = "test",
            InterviewQuestionIds = new[] { 0 }
        };

        var result = await EndpointCall(request);

        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }
}
