using Domain.Models;
using System;
using Xunit;

namespace UnitTests.Tests.Application.Models;

public class QuestionModelTest
{
    [Fact]
    public void InterviewQuestionModel_UseProperties()
    {
        DateTime now = DateTime.Now;
        var model = new QuestionModel();

        model.CreatedAt = now;
        model.UpdatedAt = now;

        Assert.Equal(now, model.CreatedAt);
        Assert.Equal(now, model.UpdatedAt);
    }
}
