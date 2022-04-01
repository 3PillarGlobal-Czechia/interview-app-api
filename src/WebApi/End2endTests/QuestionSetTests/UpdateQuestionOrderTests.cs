using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WebApi;
using WebApi.UseCases.v1.QuestionSet.UpdateQuestionOrder;
using Xunit;

namespace End2EndTests.QuestionListTests;

public class UpdateQuestionOrderTests : E2ETestsBase, IClassFixture<MyWebApplicationFactory<Startup>>
{
    private readonly Func<int, string> _url = (int id) => $"/api/v1/QuestionSet/{id}/UpdateQuestionOrder";

    public UpdateQuestionOrderTests(MyWebApplicationFactory<Startup> factory) : base(factory)
    {
    }

    private async Task<StandardResponse> EndpointCall(int id, UpdateQuestionOrderRequest request) => await _wrapper.PostAsync(_url(id), request);

    [Fact]
    public async Task UpdateQuestionOrder_ValidRequest_ReturnsOk()
    {
        var request = new UpdateQuestionOrderRequest
        {
            OrderedQuestionIds = new List<int>() { 3, 2, 1 }
        };

        var result = await EndpointCall(1, request);

        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task UpdateQuestionOrder_NonExistingRequest_ReturnsNotFound()
    {
        var request = new UpdateQuestionOrderRequest
        {
            OrderedQuestionIds = new List<int>() { 3, 2, 1 }
        };

        var result = await EndpointCall(0, request);

        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task UpdateQuestionOrder_InvalidRequest_ReturnsBadRequest()
    {
        var request = new UpdateQuestionOrderRequest
        {
            OrderedQuestionIds = new List<int>() { 0 }
        };

        var result = await EndpointCall(1, request);

        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }
}
