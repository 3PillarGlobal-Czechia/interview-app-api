using Domain.Models;
using System;
using Xunit;

namespace UnitTests.Tests.Application.Models;

public class InterviewQuestionModelTest
{
    [Fact]
    public void InterviewQuestionModel_UseProperties()
    {
        DateTime now = DateTime.Now;
        var model = new InterviewQuestionModel();

        model.CreatedAt = now;
        model.UpdatedAt = now;

        Assert.Equal(now, model.CreatedAt);
        Assert.Equal(now, model.UpdatedAt);
    }
}
