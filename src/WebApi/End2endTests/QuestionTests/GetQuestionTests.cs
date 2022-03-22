using Domain.Models;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApi;
using WebApi.UseCases.v1.Question.GetQuestion;
using Xunit;

namespace End2EndTests.InterviewQuestionTests;

public class GetQuestionTests : E2ETestsBase, IClassFixture<MyWebApplicationFactory<Startup>>
{
    private readonly string _url = "/api/v1/Question";

    public GetQuestionTests(MyWebApplicationFactory<Startup> factory) : base(factory)
    {
    }

    private async Task<StandardResponse<IEnumerable<QuestionModel>>> EndpointCall(GetQuestionRequest request)
    {
        NameValueCollection queryParams = new()
        {
            { "category", request.Category },
            { "text", request.Text }
        };
        foreach (int difficulty in request.Difficulties ?? Enumerable.Empty<int>())
        {
            queryParams.Add("difficulties", difficulty.ToString());
        }

        return await GetAsync<IEnumerable<QuestionModel>>(_url, queryParams);
    }

    [Fact]
    public async Task Get_ValidRequest_ReturnsAllQuestions()
    {
        var request = new GetQuestionRequest();

        var response = await EndpointCall(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(response.Data);
        Assert.Equal(8, response.Data.Count());
    }

    [Fact]
    public async Task Get_NonExistingRequest_ReturnsNotFound()
    {
        var request = new GetQuestionRequest
        {
            Category = "Some arbitrary category name that does not exist"
        };

        var response = await EndpointCall(request);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Get_InvalidRequest_ReturnsBadRequest()
    {
        var request = new GetQuestionRequest
        {
            Category = "Some category name that exceeds the maximum allowed number of characters"
        };

        var response = await EndpointCall(request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
