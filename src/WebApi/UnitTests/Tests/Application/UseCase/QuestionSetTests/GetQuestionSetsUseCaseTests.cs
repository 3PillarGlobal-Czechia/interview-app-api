using Application.Repositories;
using Application.UseCases.QuestionSet.GetQuestionSets;
using Domain.Models;
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
        var input = new GetQuestionSetsInput();
        var useCase = new GetQuestionSetsUseCase(null);

        var execute = async () => await useCase.Execute(input);

        await Assert.ThrowsAsync<NullReferenceException>(execute);
    }

    [Fact]
    public async Task Execute_PassNull_Throws()
    {
        var useCase = new GetQuestionSetsUseCase(It.IsAny<IQuestionSetRepository>());

        var execute = async () => await useCase.Execute(null);

        await Assert.ThrowsAsync<ArgumentNullException>(execute);
    }

    [Fact]
    public async Task Execute_RepositoryReturnsNull_NotFound()
    {
        var repositoryMock = new Mock<IQuestionSetRepository>();
        repositoryMock.Setup(x => x.GetAll()).ReturnsAsync((IEnumerable<QuestionSetModel>)null);
        var outputPortMock = new Mock<IOutputPort>();
        var input = new GetQuestionSetsInput();
        var useCase = new GetQuestionSetsUseCase(repositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(input);

        outputPortMock.Verify(x => x.Ok(It.IsAny<IEnumerable<QuestionSetModel>>()), Times.Never());
        outputPortMock.Verify(x => x.Invalid(), Times.Never());
        outputPortMock.Verify(x => x.NotFound(), Times.Once());
    }

    [Fact]
    public async Task Execute_RepositoryReturnsEmpty_NotFound()
    {
        var repositoryMock = new Mock<IQuestionSetRepository>();
        repositoryMock.Setup(x => x.GetAll()).ReturnsAsync(new List<QuestionSetModel>());
        var outputPortMock = new Mock<IOutputPort>();
        var input = new GetQuestionSetsInput();
        var useCase = new GetQuestionSetsUseCase(repositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(input);

        outputPortMock.Verify(x => x.Ok(It.IsAny<IEnumerable<QuestionSetModel>>()), Times.Never());
        outputPortMock.Verify(x => x.Invalid(), Times.Never());
        outputPortMock.Verify(x => x.NotFound(), Times.Once());
    }

    [Fact]
    public async Task Execute_RepositoryReturnsValidData_Ok()
    {
        var repositoryMock = new Mock<IQuestionSetRepository>();
        repositoryMock.Setup(x => x.GetAll()).ReturnsAsync(QuesionSets);
        var outputPortMock = new Mock<IOutputPort>();
        var input = new GetQuestionSetsInput();
        var useCase = new GetQuestionSetsUseCase(repositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(input);

        outputPortMock.Verify(x => x.Ok(It.IsAny<IEnumerable<QuestionSetModel>>()), Times.Once());
        outputPortMock.Verify(x => x.Invalid(), Times.Never());
        outputPortMock.Verify(x => x.NotFound(), Times.Never());
    }
}
