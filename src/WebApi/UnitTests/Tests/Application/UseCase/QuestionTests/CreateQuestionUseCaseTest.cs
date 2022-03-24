using Application.Repositories;
using Application.UseCases.InterviewQuestion.CreateInterviewQuestion;
using Application.UseCases.Question.CreateInterviewQuestion;
using Domain.Models;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Tests.Application.UseCase.QuestionTests;

public class CreateQuestionUseCaseTest
{
    private static CreateQuestionInput Input => new CreateQuestionInput()
    {
        Title = "test",
        Difficulty = 1,
        Category = "test",
        Content = "test"
    };

    [Fact]
    public async Task Execute_RepositoryNull_Throws()
    {
        var useCase = new CreateQuestionUseCase(null);

        var execute = async () => await useCase.Execute(Input);

        await Assert.ThrowsAsync<NullReferenceException>(execute);
    }

    [Fact]
    public async Task Execute_PassEmpty_Throws()
    {
        var useCase = new CreateQuestionUseCase(It.IsAny<IQuestionRepository>());

        var execute = async () => await useCase.Execute(It.IsAny<CreateQuestionInput>());

        await Assert.ThrowsAsync<NullReferenceException>(execute);
    }

    [Fact]
    public async Task Execute_PassValidInput_CallsOk()
    {
        var repositoryMock = new Mock<IQuestionRepository>();
        repositoryMock.Setup(x => x.Create(It.IsAny<QuestionModel>()).Result).Returns(new QuestionModel());
        var outputPortMock = new Mock<IOutputPort>();
        var useCase = new CreateQuestionUseCase(repositoryMock.Object);
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
        repositoryMock.Setup(x => x.Create(It.IsAny<QuestionModel>())).ReturnsAsync((QuestionModel)null);
        var outputPortMock = new Mock<IOutputPort>();
        var useCase = new CreateQuestionUseCase(repositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(Input);

        outputPortMock.Verify(x => x.Ok(), Times.Never());
        outputPortMock.Verify(x => x.Invalid(), Times.Once());
        outputPortMock.Verify(x => x.NotFound(), Times.Never());
    }
}
