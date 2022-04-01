using Domain.Models;
using System;
using System.Net;
using System.Threading.Tasks;
using WebApi;
using Xunit;

namespace End2EndTests.QuestionTests
{
    public class DeleteQuestionTests : E2ETestsBase, IClassFixture<MyWebApplicationFactory<Startup>>
    {
        private readonly Func<int, string> _url = (int id) => $"/api/v1/Question/{id}";

        public DeleteQuestionTests(MyWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        private async Task<StandardResponse<QuestionModel>> EndpointCall(int id) => await _wrapper.DeleteAsync<QuestionModel>(_url(id));

        [Fact]
        public async Task Create_ValidRequest_ReturnsNoContent()
        {
            var result = await EndpointCall(1);

            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async Task Create_InvalidRequest_ReturnsNotFound()
        {
            var result = await EndpointCall(0);

            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
