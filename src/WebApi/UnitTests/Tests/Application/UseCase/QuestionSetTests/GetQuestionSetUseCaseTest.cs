using Application.Repositories;
using Application.UseCases.QuestionSet.GetQuestionList;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Tests.Application.UseCase.QuestionSetTests;

public class GetQuestionSetUseCaseTest
{
    private static GetQuestionSetInput Input => new GetQuestionSetInput()
    {
        Id = 1
    };

    private static List<QuestionModel> Questions => new List<QuestionModel>()
    {
        new QuestionModel() {
            Id = 1,
            Category = "first category",
            Title = "C# question",
            Content = "just some test content",
            Difficulty = 1
        },
        new QuestionModel() {
            Id = 2,
            Category = "second category",
            Title = ".NET question",
            Content = "another one, definitely not a test",
            Difficulty = 1
        },
        new QuestionModel() {
            Id = 3,
            Category = "third category",
            Title = "SQL question",
            Content = "test",
            Difficulty = 3
        }
    };

    private static List<QuestionSetModel> Data => new List<QuestionSetModel>()
    {
        new QuestionSetModel() {
            Id = 1,
            Title = "list1",
            Description = "easy peasy lemon squeezy",
            InterviewQuestions = Questions.Where(x => x.Difficulty == 1).ToList()
        },
        new QuestionSetModel() {
            Id = 2,
            Title = "list2",
            Description = "definitely not testing questions",
            InterviewQuestions = Questions.Where(x => x.Content.Contains("not a test")).ToList()
        },
        new QuestionSetModel() {
            Id = 3,
            Title = "list3",
            Description = "all except id 1",
            InterviewQuestions = Questions.Where(x => x.Id != 1).ToList()
        }
    };

    [Fact]
    public async Task Execute_RepositoryNull_Throws()
    {
        var useCase = new GetQuestionSetUseCase(null);

        var execute = async () => await useCase.Execute(Input);

        await Assert.ThrowsAsync<NullReferenceException>(execute);
    }

    [Fact]
    public async Task Execute_PassNull_Throws()
    {
        var useCase = new GetQuestionSetUseCase(It.IsAny<IQuestionSetRepository>());

        var execute = async () => await useCase.Execute(null);

        await Assert.ThrowsAsync<ArgumentNullException>(execute);
    }

    [Fact]
    public async Task Execute_PassValidInput_CallsOk()
    {
        var repositoryMock = new Mock<IQuestionSetRepository>();
        repositoryMock.Setup(x => x.GetById(It.IsAny<int>()).Result).Returns(Data[0]);
        var outputPortMock = new Mock<IOutputPort>();
        var useCase = new GetQuestionSetUseCase(repositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(Input);

        outputPortMock.Verify(x => x.Ok(It.IsAny<QuestionSetModel>()), Times.Once());
        outputPortMock.Verify(x => x.Invalid(), Times.Never());
        outputPortMock.Verify(x => x.NotFound(), Times.Never());
    }

    [Fact]
    public async Task Execute_PassValidInput_CallsNotFound()
    {
        var repositoryMock = new Mock<IQuestionSetRepository>();
        repositoryMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((QuestionSetModel)null);
        var outputPortMock = new Mock<IOutputPort>();
        var useCase = new GetQuestionSetUseCase(repositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(Input);

        outputPortMock.Verify(x => x.Ok(It.IsAny<QuestionSetModel>()), Times.Never());
        outputPortMock.Verify(x => x.Invalid(), Times.Never());
        outputPortMock.Verify(x => x.NotFound(), Times.Once());
    }
}
