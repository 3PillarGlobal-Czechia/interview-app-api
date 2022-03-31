﻿using Domain.Models;
using System;
using System.Net;
using System.Threading.Tasks;
using WebApi;
using WebApi.UseCases.v1.QuestionSet.DeleteQuestionSet;
using Xunit;

namespace End2EndTests.QuestionSetTests
{
    public class DeleteQuestionSetTests : E2ETestsBase, IClassFixture<MyWebApplicationFactory<Startup>>
    {
        private readonly Func<int, string> _url = (int id) => $"/api/v1/QuestionSet/{id}";

        public DeleteQuestionSetTests(MyWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        private async Task<StandardResponse<QuestionSetModel>> EndpointCall(int id) => await _wrapper.DeleteAsync<QuestionSetModel>(_url(id));

        [Fact]
        public async Task Delete_ValidRequest_ReturnsOk()
        {
            var result = await EndpointCall(1);

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task Delete_InvalidRequest_ReturnsNotFound()
        {          
            var result = await EndpointCall(0);

            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
