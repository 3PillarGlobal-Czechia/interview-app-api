using Application.Repositories;
using Application.UseCases.QuestionSet.GetQuestionSets;
using Domain.Models;
using Domain.Models.Views;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Tests.Application.UseCase.QuestionSetTests;

public class GetQuestionSetsUseCaseTests
{
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

    private static List<QuestionSetModel> QuesionSets => new List<QuestionSetModel>()
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
        var input = new GetQuestionSetsInput();
        var useCase = new GetQuestionSetsUseCase(null,null);

        var execute = async () => await useCase.Execute(input);

        await Assert.ThrowsAsync<NullReferenceException>(execute);
    }

    [Fact]
    public async Task Execute_PassEmpty_Throws()
    {
        var useCase = new GetQuestionSetsUseCase(It.IsAny<IQuestionSetRepository>(), It.IsAny<IQuestionRepository>());

        var execute = async () => await useCase.Execute(It.IsAny<GetQuestionSetsInput>());

        await Assert.ThrowsAsync<NullReferenceException>(execute);
    }

    [Fact]
    public async Task Execute_RepositoryReturnsNull_NotFound()
    {
        var questionSetRepositoryMock = new Mock<IQuestionSetRepository>();
        questionSetRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync((IEnumerable<QuestionSetModel>)null);

        var questionRepositoryMock = new Mock<IQuestionRepository>();
        questionRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync((IEnumerable<QuestionModel>)null);

        var outputPortMock = new Mock<IOutputPort>();
        var input = new GetQuestionSetsInput();
        var useCase = new GetQuestionSetsUseCase(questionSetRepositoryMock.Object, questionRepositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(input);

        outputPortMock.Verify(x => x.Ok(It.IsAny<IEnumerable<QuestionSetListItem>>()), Times.Never());
        outputPortMock.Verify(x => x.Invalid(), Times.Never());
        outputPortMock.Verify(x => x.NotFound(), Times.Once());
    }

    [Fact]
    public async Task Execute_RepositoryReturnsEmpty_NotFound()
    {
        var questionSetRepositoryMock = new Mock<IQuestionSetRepository>();
        questionSetRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(new List<QuestionSetModel>());

        var questionRepositoryMock = new Mock<IQuestionRepository>();
        questionRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(new List<QuestionModel>());

        var outputPortMock = new Mock<IOutputPort>();
        var input = new GetQuestionSetsInput();
        var useCase = new GetQuestionSetsUseCase(questionSetRepositoryMock.Object, questionRepositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(input);

        outputPortMock.Verify(x => x.Ok(It.IsAny<IEnumerable<QuestionSetListItem>>()), Times.Never());
        outputPortMock.Verify(x => x.Invalid(), Times.Never());
        outputPortMock.Verify(x => x.NotFound(), Times.Once());
    }

    [Fact]
    public async Task Execute_RepositoryReturnsValidData_Ok()
    {
        var questionSetRepositoryMock = new Mock<IQuestionSetRepository>();
        questionSetRepositoryMock.Setup(x => x.GetAll().Result).Returns(QuesionSets);

        var questionRepositoryMock = new Mock<IQuestionRepository>();
        for(int i = 1; i < 4; i++) { 
            questionRepositoryMock.Setup(x => x.GetQuestionsBySetId(i).Result).Returns(Questions);
        }

        var outputPortMock = new Mock<IOutputPort>();
        var input = new GetQuestionSetsInput();
        var useCase = new GetQuestionSetsUseCase(questionSetRepositoryMock.Object, questionRepositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(input);

        outputPortMock.Verify(x => x.Ok(It.IsAny<IEnumerable<QuestionSetListItem>>()), Times.Once());
        outputPortMock.Verify(x => x.Invalid(), Times.Never());
        outputPortMock.Verify(x => x.NotFound(), Times.Never());
    }

    [Fact]
    public async Task Execute_RepositoryReturnsNoQuestions_Ok()
    {
        var questionSetRepositoryMock = new Mock<IQuestionSetRepository>();
        questionSetRepositoryMock.Setup(x => x.GetAll().Result).Returns(QuesionSets);

        var questionRepositoryMock = new Mock<IQuestionRepository>();

        var outputPortMock = new Mock<IOutputPort>();
        outputPortMock.Setup(x => x.Ok(It.IsAny<IEnumerable<QuestionSetListItem>>()))
           .Callback<IEnumerable<QuestionSetListItem>>(result => Assert.Equal(0, result.First().Difficulty.Value));

        var input = new GetQuestionSetsInput();
        var useCase = new GetQuestionSetsUseCase(questionSetRepositoryMock.Object, questionRepositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(input);

        outputPortMock.Verify(x => x.Ok(It.IsAny<IEnumerable<QuestionSetListItem>>()), Times.Once());
        outputPortMock.Verify(x => x.Invalid(), Times.Never());
        outputPortMock.Verify(x => x.NotFound(), Times.Never());
    }
}
