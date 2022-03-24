using Application.Repositories;
using Application.UseCases.QuestionSet.GetQuestionSet;
using Domain.Models;
using Domain.Models.Agreggates;
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
        },
        new QuestionSetModel() {
            Id = 2,
            Title = "list2",
            Description = "definitely not testing questions",
        },
        new QuestionSetModel() {
            Id = 3,
            Title = "list3",
            Description = "all except id 1",
        }
    };

    [Fact]
    public async Task Execute_RepositoryNull_Throws()
    {
        var useCase = new GetQuestionSetUseCase(null, null);

        var execute = async () => await useCase.Execute(Input);

        await Assert.ThrowsAsync<NullReferenceException>(execute);
    }

    [Fact]
    public async Task Execute_PassEmpty_Throws()
    {
        var useCase = new GetQuestionSetUseCase(It.IsAny<IQuestionSetRepository>(), It.IsAny<IQuestionRepository>());

        var execute = async () => await useCase.Execute(It.IsAny<GetQuestionSetInput>());

        await Assert.ThrowsAsync<NullReferenceException>(execute);
    }

    [Fact]
    public async Task Execute_PassValidInput_CallsOk()
    {
        var questionSetRepositoryMock = new Mock<IQuestionSetRepository>();
        questionSetRepositoryMock.Setup(x => x.GetById(It.IsAny<int>()).Result).Returns(Data[0]);

        var questionRepositoryMock = new Mock<IQuestionRepository>();

        var outputPortMock = new Mock<IOutputPort>();
        var useCase = new GetQuestionSetUseCase(questionSetRepositoryMock.Object, questionRepositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(Input);

        outputPortMock.Verify(x => x.Ok(It.IsAny<QuestionSetDetail>()), Times.Once());
        outputPortMock.Verify(x => x.Invalid(), Times.Never());
        outputPortMock.Verify(x => x.NotFound(), Times.Never());
    }

    [Fact]
    public async Task Execute_PassValidInput_CallsNotFound()
    {
        var questionSetRepositoryMock = new Mock<IQuestionSetRepository>();
        questionSetRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((QuestionSetModel)null);

        var questionRepositoryMock = new Mock<IQuestionRepository>();
        var outputPortMock = new Mock<IOutputPort>();
        var useCase = new GetQuestionSetUseCase(questionSetRepositoryMock.Object, questionRepositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(Input);

        outputPortMock.Verify(x => x.Ok(It.IsAny<QuestionSetDetail>()), Times.Never());
        outputPortMock.Verify(x => x.Invalid(), Times.Never());
        outputPortMock.Verify(x => x.NotFound(), Times.Once());
    }
}
