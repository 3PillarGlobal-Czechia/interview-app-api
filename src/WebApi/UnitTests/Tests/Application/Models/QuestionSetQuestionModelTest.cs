using Domain.Models;
using Xunit;

namespace UnitTests.Tests.Application.Models;

public class QuestionSetQuestionModelTest
{
    [Fact]
    public void QuestionSetQuestionModel_UseProperties()
    {
        var model = new QuestionSetQuestionModel
        {
            InterviewQuestionId = 0,
            QuestionListId = 0,
            QuestionList = null,
            InterviewQuestion = null,
            Order = 0
        };

        Assert.NotNull(model);
    }
}
