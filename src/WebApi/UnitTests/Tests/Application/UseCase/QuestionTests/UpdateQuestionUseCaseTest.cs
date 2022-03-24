using Application.Repositories;
using Application.UseCases.InterviewQuestion.UpdateInterviewQuestion;
using Domain.Models;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Tests.Application.UseCase.QuestionTests;

public class UpdateQuestionUseCaseTest
{
    private static UpdateInterviewQuestionInput Input => new UpdateInterviewQuestionInput()
    {
        Id = 1,
        Title = "test",
        Difficulty = 1,
        Category = "test",
        Content = "test"
    };

    private static QuestionModel Model => new QuestionModel()
    {
        Id = 1,
        Title = "test",
        Difficulty = 1,
        Category = "test",
        Content = "test"
    };

    [Fact]
    public async Task Execute_RepositoryNull_Throws()
    {
        var useCase = new UpdateInterviewQuestionUseCase(null);

        var execute = async () => await useCase.Execute(Input);

        await Assert.ThrowsAsync<NullReferenceException>(execute);
    }

    [Fact]
    public async Task Execute_PassEmpty_Throws()
    {
        var useCase = new UpdateInterviewQuestionUseCase(It.IsAny<IQuestionRepository>());

        var execute = async () => await useCase.Execute(It.IsAny<UpdateInterviewQuestionInput>());

        await Assert.ThrowsAsync<NullReferenceException>(execute);
    }

    [Fact]
    public async Task Execute_PassValidInput_CallsOk()
    {
        var repositoryMock = new Mock<IQuestionRepository>();
        repositoryMock.Setup(x => x.GetById(It.IsAny<int>()).Result).Returns(Model);
        repositoryMock.Setup(x => x.Update(It.IsAny<QuestionModel>()).Result).Returns(true);
        var outputPortMock = new Mock<IOutputPort>();
        var useCase = new UpdateInterviewQuestionUseCase(repositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(Input);

        outputPortMock.Verify(x => x.Ok(), Times.Once());
        outputPortMock.Verify(x => x.Invalid(), Times.Never());
        outputPortMock.Verify(x => x.NotFound(), Times.Never());
    }

    [Fact]
    public async Task Execute_PassValidInput_CallsInvalid()
    {
        var repositoryMock = new Mock<IQuestionRepository>();
        repositoryMock.Setup(x => x.GetById(It.IsAny<int>()).Result).Returns(Model);
        repositoryMock.Setup(x => x.Update(It.IsAny<QuestionModel>()).Result).Returns(false);
        var outputPortMock = new Mock<IOutputPort>();
        var useCase = new UpdateInterviewQuestionUseCase(repositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(Input);

        outputPortMock.Verify(x => x.Ok(), Times.Never());
        outputPortMock.Verify(x => x.Invalid(), Times.Once());
        outputPortMock.Verify(x => x.NotFound(), Times.Never());
    }

    [Fact]
    public async Task Execute_PassNotExistingInput_CallsNotFound()
    {
        var repositoryMock = new Mock<IQuestionRepository>();
        repositoryMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((QuestionModel)null);
        var outputPortMock = new Mock<IOutputPort>();
        var useCase = new UpdateInterviewQuestionUseCase(repositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(Input);

        outputPortMock.Verify(x => x.Ok(), Times.Never());
        outputPortMock.Verify(x => x.Invalid(), Times.Never());
        outputPortMock.Verify(x => x.NotFound(), Times.Once());
    }
}
