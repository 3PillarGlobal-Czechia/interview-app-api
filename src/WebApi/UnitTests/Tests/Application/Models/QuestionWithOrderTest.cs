using Domain.Models;
using Domain.Models.Agreggates;
using Xunit;

namespace UnitTests.Tests.Application.Models;

public class QuestionWithOrderTest
{
    [Fact]
    public void QuestionWithOrder_UseProperties()
    {
        var question = new QuestionWithOrder();

        question.Question = new QuestionModel();
        question.Order = 1; 

        Assert.NotNull(question?.Question);
    }
}

