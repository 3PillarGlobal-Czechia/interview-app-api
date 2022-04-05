using Application.Repositories;
using Application.UseCases.Question.DeleteInterviewQuestion;
using Domain.Models;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Tests.Application.UseCase.QuestionTests;

public class DeleteQuestionUseCaseTest
{
    private static DeleteQuestionInput Input => new DeleteQuestionInput()
    {
        Id= 1
    };

    private static QuestionModel Model => new QuestionModel()
    {
        Title = "test",
        Difficulty = 1,
        Category = "test",
        Content = "test"
    };

    [Fact]
    public async Task Execute_RepositoryNull_Throws()
    {
        var useCase = new DeleteQuestionUseCase(null);

        var execute = async () => await useCase.Execute(Input);

        await Assert.ThrowsAsync<NullReferenceException>(execute);
    }

    [Fact]
    public async Task Execute_PassEmpty_Throws()
    {
        var useCase = new DeleteQuestionUseCase(It.IsAny<IQuestionRepository>());

        var execute = async () => await useCase.Execute(It.IsAny<DeleteQuestionInput>());

        await Assert.ThrowsAsync<NullReferenceException>(execute);
    }

    [Fact]
    public async Task Execute_PassValidInput_CallsNoContent()
    {
        var repositoryMock = new Mock<IQuestionRepository>();
        repositoryMock.Setup(x => x.GetById(1)).ReturnsAsync(new QuestionModel());
        repositoryMock.Setup(x => x.Delete(1)).ReturnsAsync(true);
        var outputPortMock = new Mock<IOutputPort>();
        var useCase = new DeleteQuestionUseCase(repositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(Input);

        outputPortMock.Verify(x => x.NoContent(), Times.Once());
        outputPortMock.Verify(x => x.Invalid(), Times.Never());
        outputPortMock.Verify(x => x.NotFound(), Times.Never());
    }

    [Fact]
    public async Task Execute_PassValidInput_CallsNotFound()
    {
        var repositoryMock = new Mock<IQuestionRepository>();
        repositoryMock.Setup(x => x.GetById(1)).ReturnsAsync((QuestionModel)null);
        var outputPortMock = new Mock<IOutputPort>();
        var useCase = new DeleteQuestionUseCase(repositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(Input);

        outputPortMock.Verify(x => x.NoContent(), Times.Never());
        outputPortMock.Verify(x => x.Invalid(), Times.Never());
        outputPortMock.Verify(x => x.NotFound(), Times.Once());
    }


    [Fact]
    public async Task Execute_PassValidInput_CallsInvalid()
    {
        var repositoryMock = new Mock<IQuestionRepository>();
        repositoryMock.Setup(x => x.GetById(1)).ReturnsAsync(Model);
        repositoryMock.Setup(x => x.Delete(1)).ReturnsAsync(false);
        var outputPortMock = new Mock<IOutputPort>();
        var useCase = new DeleteQuestionUseCase(repositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(Input);

        outputPortMock.Verify(x => x.NoContent(), Times.Never());
        outputPortMock.Verify(x => x.Invalid(), Times.Once());
        outputPortMock.Verify(x => x.NotFound(), Times.Never());
    }
}
