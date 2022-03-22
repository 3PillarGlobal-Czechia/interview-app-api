using System.Net;
using System.Threading.Tasks;
using WebApi;
using WebApi.UseCases.v1.Question.CreateQuestion;
using Xunit;

namespace End2EndTests.InterviewQuestionTests;

public class CreateQuestionTests : E2ETestsBase, IClassFixture<MyWebApplicationFactory<Startup>>
{
    private readonly string _url = "/api/v1/Question";

    public CreateQuestionTests(MyWebApplicationFactory<Startup> factory) : base(factory)
    {
    }

    private async Task<StandardResponse> EndpointCall(CreateQuestionRequest request) => await PostAsync(_url, request);

    [Fact]
    public async Task Create_ValidRequest_ReturnsOk()
    {
        var request = new CreateQuestionRequest
        {
            Title = "test",
            Content = "just some content",
            Difficulty = 1,
            Category = "C# concept basics"
        };

        var result = await EndpointCall(request);

        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task Create_InvalidRequest_ReturnsBadRequest()
    {
        var request = new CreateQuestionRequest();

        var result = await EndpointCall(request);

        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }
}
