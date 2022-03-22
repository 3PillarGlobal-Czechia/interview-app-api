using Application.Repositories;
using Application.UseCases.InterviewQuestion.GetInterviewQuestion;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Tests.Application.UseCase.QuestionTests;

public class GetQuestionUseCaseTest
{
    private static GetInterviewQuestionInput Input => new GetInterviewQuestionInput()
    {
        Difficulties = new[] { 1, 2, 3 },
        Text = "test"
    };

    private static readonly List<QuestionModel> Data = new List<QuestionModel>()
    {
        new QuestionModel() {
            Id = 1,
            Category = "cat1",
            Title = "question1",
            Content = "just some test content",
            Difficulty = 1
        },
        new QuestionModel() {
            Id = 2,
            Category = "cat2",
            Title = "question2",
            Content = "another one, definitely not a test",
            Difficulty = 1
        },
        new QuestionModel() {
            Id = 3,
            Category = "cat1",
            Title = "question3",
            Content = "test",
            Difficulty = 3
        }
    };

    [Fact]
    public async Task Execute_RepositoryNull_Throws()
    {
        var useCase = new GetInterviewQuestionUseCase(null);

        var execute = async () => await useCase.Execute(Input);

        await Assert.ThrowsAsync<NullReferenceException>(execute);
    }

    [Fact]
    public async Task Execute_PassEmpty_Throws()
    {
        var useCase = new GetInterviewQuestionUseCase(It.IsAny<IInterviewQuestionRepository>());

        var execute = async () => await useCase.Execute(It.IsAny<GetInterviewQuestionInput>());

        await Assert.ThrowsAsync<NullReferenceException>(execute);
    }

    [Fact]
    public async Task Execute_PassValidInput_CallsOk()
    {
        var repositoryMock = new Mock<IInterviewQuestionRepository>();
        repositoryMock.Setup(x => x.Get(It.IsAny<GetInterviewQuestionInput>()).Result).Returns(Data);
        var outputPortMock = new Mock<IOutputPort>();
        var useCase = new GetInterviewQuestionUseCase(repositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(Input);

        outputPortMock.Verify(x => x.Ok(Data), Times.Once());
        outputPortMock.Verify(x => x.Invalid(), Times.Never());
        outputPortMock.Verify(x => x.NotFound(), Times.Never());
    }

    [Fact]
    public async Task Execute_PassValidInput_CallsNotFound()
    {
        var repositoryMock = new Mock<IInterviewQuestionRepository>();
        repositoryMock.Setup(x => x.Get(It.IsAny<GetInterviewQuestionInput>())).ReturnsAsync((IEnumerable<QuestionModel>)null);
        var outputPortMock = new Mock<IOutputPort>();
        var useCase = new GetInterviewQuestionUseCase(repositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(Input);

        outputPortMock.Verify(x => x.Ok(It.IsAny<IEnumerable<QuestionModel>>()), Times.Never());
        outputPortMock.Verify(x => x.Invalid(), Times.Never());
        outputPortMock.Verify(x => x.NotFound(), Times.Once());
    }
}
