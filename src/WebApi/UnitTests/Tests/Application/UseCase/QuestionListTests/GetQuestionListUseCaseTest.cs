using Application.Repositories;
using Application.UseCases.QuestionList.GetQuestionList;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Tests.Application.UseCase.QuestionListTests;

public class GetQuestionListUseCaseTest
{
    private static GetQuestionListInput Input => new GetQuestionListInput()
    {
        Text = "test",
        Categories = new[] { "C#" }
    };

    private static List<InterviewQuestionModel> Questions => new List<InterviewQuestionModel>()
    {
        new InterviewQuestionModel() {
            Id = 1,
            Category = "first category",
            Title = "C# question",
            Content = "just some test content",
            Difficulty = 1
        },
        new InterviewQuestionModel() {
            Id = 2,
            Category = "second category",
            Title = ".NET question",
            Content = "another one, definitely not a test",
            Difficulty = 1
        },
        new InterviewQuestionModel() {
            Id = 3,
            Category = "third category",
            Title = "SQL question",
            Content = "test",
            Difficulty = 3
        }
    };

    private static List<QuestionListModel> Data => new List<QuestionListModel>()
    {
        new QuestionListModel() {
            Id = 1,
            Title = "list1",
            Description = "easy peasy lemon squeezy",
            InterviewQuestions = Questions.Where(x => x.Difficulty == 1).ToList()
        },
        new QuestionListModel() {
            Id = 2,
            Title = "list2",
            Description = "definitely not testing questions",
            InterviewQuestions = Questions.Where(x => x.Content.Contains("not a test")).ToList()
        },
        new QuestionListModel() {
            Id = 3,
            Title = "list3",
            Description = "all except id 1",
            InterviewQuestions = Questions.Where(x => x.Id != 1).ToList()
        }
    };

    [Fact]
    public async Task Execute_RepositoryNull_Throws()
    {
        var useCase = new GetQuestionListUseCase(null);

        var execute = async () => await useCase.Execute(Input);

        await Assert.ThrowsAsync<NullReferenceException>(execute);
    }

    [Fact]
    public async Task Execute_PassNull_Throws()
    {
        var useCase = new GetQuestionListUseCase(It.IsAny<IQuestionListRepository>());

        var execute = async () => await useCase.Execute(null);

        await Assert.ThrowsAsync<ArgumentNullException>(execute);
    }

    [Fact]
    public async Task Execute_PassValidInput_CallsOk()
    {
        var repositoryMock = new Mock<IQuestionListRepository>();
        repositoryMock.Setup(x => x.Get(It.IsAny<GetQuestionListInput>()).Result).Returns(Data);
        var outputPortMock = new Mock<IOutputPort>();
        var useCase = new GetQuestionListUseCase(repositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(Input);

        outputPortMock.Verify(x => x.Ok(It.IsAny<IEnumerable<QuestionListModel>>()), Times.Once());
        outputPortMock.Verify(x => x.Invalid(), Times.Never());
        outputPortMock.Verify(x => x.NotFound(), Times.Never());
    }

    [Fact]
    public async Task Execute_PassValidInput_CallsNotFound()
    {
        var repositoryMock = new Mock<IQuestionListRepository>();
        repositoryMock.Setup(x => x.Get(It.IsAny<GetQuestionListInput>())).ReturnsAsync((IEnumerable<QuestionListModel>)null);
        var outputPortMock = new Mock<IOutputPort>();
        var useCase = new GetQuestionListUseCase(repositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(Input);

        outputPortMock.Verify(x => x.Ok(It.IsAny<IEnumerable<QuestionListModel>>()), Times.Never());
        outputPortMock.Verify(x => x.Invalid(), Times.Never());
        outputPortMock.Verify(x => x.NotFound(), Times.Once());
    }
}
