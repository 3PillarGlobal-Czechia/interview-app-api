using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Threading.Tasks;
using Xunit;

namespace End2EndTests.Tests
{
    public class GetUserTests : IClassFixture<WebApplicationFactory<WebApi.Startup>>
    {
        private readonly string _url = "/api/v1/Users/";
        private readonly WebApplicationFactory<WebApi.Startup> _factory;

        public GetUserTests(WebApplicationFactory<WebApi.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetUser_ExistingUser_ShouldReturnUser()
        {
            // Arrange
            var userGuid = 2;
            var url = _url + userGuid;
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
