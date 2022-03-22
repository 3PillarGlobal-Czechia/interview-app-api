using Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi;
using Xunit;

namespace End2EndTests.QuestionListTests;

public class GetQuestionSetsTests : E2ETestsBase, IClassFixture<MyWebApplicationFactory<Startup>>
{
    private readonly string _url = "/api/v1/QuestionSet";

    public GetQuestionSetsTests(MyWebApplicationFactory<Startup> factory) : base(factory)
    {
    }

    private async Task<StandardResponse<IEnumerable<QuestionSetModel>>> EndpointCall() => await GetAsync<IEnumerable<QuestionSetModel>>(_url);

    [Fact]
    public async Task Get_ValidRequest_ReturnsAllLists()
    {
        var result = await EndpointCall();

        Assert.Equal(3, result.Data.Count());
    }
}
