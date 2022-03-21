using Domain.Models;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApi;
using WebApi.UseCases.v1.QuestionList.GetQuestionList;
using Xunit;

namespace End2EndTests.QuestionListTests;

public class GetQuestionListsTests : E2ETestsBase, IClassFixture<MyWebApplicationFactory<Startup>>
{
    private readonly string _url = "/api/v1/QuestionLists";

    public GetQuestionListsTests(MyWebApplicationFactory<Startup> factory) : base(factory)
    {
    }

    private async Task<StandardResponse<IEnumerable<QuestionListModel>>> EndpointCall(GetQuestionListRequest request)
    {
        NameValueCollection queryParams = new()
        {
            { "id", request.Id.ToString() },
            { "text", request.Text },
        };
        foreach (string category in request.Categories ?? Enumerable.Empty<string>())
        {
            queryParams.Add("categories", category);
        }

        return await GetAsync<IEnumerable<QuestionListModel>>(_url, queryParams);
    }

    [Fact]
    public async Task Get_ValidRequest_ReturnsAllLists()
    {
        var request = new GetQuestionListRequest();

        var result = await EndpointCall(request);

        Assert.Equal(3, result.Data.Count());
    }

    [Fact]
    public async Task Get_ById_ReturnsSingleList()
    {
        var request = new GetQuestionListRequest
        {
            Id = 1,
        };

        var result = await EndpointCall(request);

        Assert.Single(result.Data);
    }

    [Fact]
    public async Task Get_NonExistingRequest_ReturnsNotFound()
    {
        var request = new GetQuestionListRequest
        {
            Id = 0,
        };

        var result = await EndpointCall(request);

        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}
