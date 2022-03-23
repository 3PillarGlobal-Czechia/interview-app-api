using Application.Repositories;
using Application.UseCases.QuestionSet.UpdateQuestionSet;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Tests.Application.UseCase.QuestionSetTests;

public class UpdateQuestionSetUseCaseTest
{
    private static UpdateQuestionSetInput Input => new UpdateQuestionSetInput()
    {
        Id = 1,
        Title = "test",
        Description = "testing list",
        QuestionsToAdd = Enumerable.Empty<int>(),
        QuestionsToRemove = Enumerable.Empty<int>()
    };

    private static QuestionSetModel Model => new QuestionSetModel()
    {
        Id = 1,
        Title = "test",
        Description = "my list",
        InterviewQuestions = new List<QuestionModel>()
        {
            new QuestionModel()
            {
                Id = 1,
                Title = "test",
                Category = "C# concept basics",
                Difficulty = 3,
                Content = "null"
            },
            new QuestionModel()
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
        var useCase = new UpdateQuestionSetUseCase(null);

        var execute = async () => await useCase.Execute(Input);

        await Assert.ThrowsAsync<NullReferenceException>(execute);
    }

    [Fact]
    public async Task Execute_PassEmpty_Throws()
    {
        var useCase = new UpdateQuestionSetUseCase(It.IsAny<IQuestionSetRepository>());

        var execute = async () => await useCase.Execute(It.IsAny<UpdateQuestionSetInput>());

        await Assert.ThrowsAsync<NullReferenceException>(execute);
    }

    [Fact]
    public async Task Execute_PassValidInput_CallsOk()
    {
        var repositoryMock = new Mock<IQuestionSetRepository>();
        repositoryMock.Setup(x => x.GetById(It.IsAny<int>()).Result).Returns(Model);
        repositoryMock.Setup(x => x.Update(It.IsAny<QuestionSetModel>()).Result).Returns(true);
        var outputPortMock = new Mock<IOutputPort>();
        var useCase = new UpdateQuestionSetUseCase(repositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(Input);

        outputPortMock.Verify(x => x.Ok(), Times.Once());
        outputPortMock.Verify(x => x.Invalid(), Times.Never());
        outputPortMock.Verify(x => x.NotFound(), Times.Never());
    }

    [Fact]
    public async Task Execute_PassValidInput_CallsInvalid()
    {
        var repositoryMock = new Mock<IQuestionSetRepository>();
        repositoryMock.Setup(x => x.GetById(It.IsAny<int>()).Result).Returns(Model);
        repositoryMock.Setup(x => x.Update(It.IsAny<QuestionSetModel>()).Result).Returns(false);
        var outputPortMock = new Mock<IOutputPort>();
        var useCase = new UpdateQuestionSetUseCase(repositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(Input);

        outputPortMock.Verify(x => x.Ok(), Times.Never());
        outputPortMock.Verify(x => x.Invalid(), Times.Once());
        outputPortMock.Verify(x => x.NotFound(), Times.Never());
    }

    [Fact]
    public async Task Execute_PassNotExistingInput_CallsNotFound()
    {
        var repositoryMock = new Mock<IQuestionSetRepository>();
        repositoryMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((QuestionSetModel)null);
        var outputPortMock = new Mock<IOutputPort>();
        var useCase = new UpdateQuestionSetUseCase(repositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(Input);

        outputPortMock.Verify(x => x.Ok(), Times.Never());
        outputPortMock.Verify(x => x.Invalid(), Times.Never());
        outputPortMock.Verify(x => x.NotFound(), Times.Once());
    }

    [Fact]
    public async Task Execute_UpdateWithAddAndRemove_Ok()
    {
        var input = new UpdateQuestionSetInput()
        {
            Id = 1,
            Title = "test",
            Description = "testing list",
            QuestionsToAdd = new List<int> { 1, 2 },
            QuestionsToRemove = new List<int> { 3, 4 }
        };

        var repositoryMock = new Mock<IQuestionSetRepository>();
        repositoryMock.Setup(x => x.GetById(It.IsAny<int>()).Result).Returns(Model);
        repositoryMock.Setup(x => x.Update(It.IsAny<QuestionSetModel>()).Result).Returns(true);
        repositoryMock.Setup(x => x.AddQuestionsToList(It.IsAny<QuestionSetModel>(), It.IsAny<IEnumerable<int>>()).Result).Returns(true);
        repositoryMock.Setup(x => x.RemoveQuestionsFromList(It.IsAny<QuestionSetModel>(), It.IsAny<IEnumerable<int>>()).Result).Returns(true);

        var outputPortMock = new Mock<IOutputPort>();
        var useCase = new UpdateQuestionSetUseCase(repositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(input);

        outputPortMock.Verify(x => x.Ok(), Times.Once());
        outputPortMock.Verify(x => x.Invalid(), Times.Never());
        outputPortMock.Verify(x => x.NotFound(), Times.Never());
    }
}
