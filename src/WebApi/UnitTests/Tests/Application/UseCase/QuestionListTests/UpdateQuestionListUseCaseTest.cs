using Application.Repositories;
using Application.UseCases.QuestionList.UpdateQuestionList;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Tests.Application.UseCase.QuestionListTests;

public class UpdateQuestionListUseCaseTest
{
    private static UpdateQuestionListInput Input => new UpdateQuestionListInput()
    {
        Id = 1,
        Title = "test",
        Description = "testing list",
        QuestionsToAdd = Enumerable.Empty<int>(),
        QuestionsToRemove = Enumerable.Empty<int>()
    };

    private static QuestionListModel Model => new QuestionListModel()
    {
        Id = 1,
        Title = "test",
        Description = "my list",
        InterviewQuestions = new List<InterviewQuestionModel>()
        {
            new InterviewQuestionModel()
            {
                Id = 1,
                Title = "test",
                Category = "C# concept basics",
                Difficulty = 3,
                Content = "null"
            },
            new InterviewQuestionModel()
            {
                Id = 2,
                Title = "another question",
                Category = ".NET specific",
                Difficulty = 1,
                Content = "just some content"
            }
        }
    };

    [Fact]
    public async Task Execute_RepositoryNull_Throws()
    {
        var useCase = new UpdateQuestionListUseCase(null);

        var execute = async () => await useCase.Execute(Input);

        await Assert.ThrowsAsync<NullReferenceException>(execute);
    }

    [Fact]
    public async Task Execute_PassNull_Throws()
    {
        var useCase = new UpdateQuestionListUseCase(It.IsAny<IQuestionListRepository>());

        var execute = async () => await useCase.Execute(null);

        await Assert.ThrowsAsync<ArgumentNullException>(execute);
    }

    [Fact]
    public async Task Execute_PassValidInput_CallsOk()
    {
        var repositoryMock = new Mock<IQuestionListRepository>();
        repositoryMock.Setup(x => x.GetById(It.IsAny<int>()).Result).Returns(Model);
        repositoryMock.Setup(x => x.Update(It.IsAny<QuestionListModel>()).Result).Returns(true);
        var outputPortMock = new Mock<IOutputPort>();
        var useCase = new UpdateQuestionListUseCase(repositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(Input);

        outputPortMock.Verify(x => x.Ok(), Times.Once());
        outputPortMock.Verify(x => x.Invalid(), Times.Never());
        outputPortMock.Verify(x => x.NotFound(), Times.Never());
    }

    [Fact]
    public async Task Execute_PassValidInput_CallsInvalid()
    {
        var repositoryMock = new Mock<IQuestionListRepository>();
        repositoryMock.Setup(x => x.GetById(It.IsAny<int>()).Result).Returns(Model);
        repositoryMock.Setup(x => x.Update(It.IsAny<QuestionListModel>()).Result).Returns(false);
        var outputPortMock = new Mock<IOutputPort>();
        var useCase = new UpdateQuestionListUseCase(repositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(Input);

        outputPortMock.Verify(x => x.Ok(), Times.Never());
        outputPortMock.Verify(x => x.Invalid(), Times.Once());
        outputPortMock.Verify(x => x.NotFound(), Times.Never());
    }

    [Fact]
    public async Task Execute_PassNotExistingInput_CallsNotFound()
    {
        var repositoryMock = new Mock<IQuestionListRepository>();
        repositoryMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((QuestionListModel)null);
        var outputPortMock = new Mock<IOutputPort>();
        var useCase = new UpdateQuestionListUseCase(repositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(Input);

        outputPortMock.Verify(x => x.Ok(), Times.Never());
        outputPortMock.Verify(x => x.Invalid(), Times.Never());
        outputPortMock.Verify(x => x.NotFound(), Times.Once());
    }
}
