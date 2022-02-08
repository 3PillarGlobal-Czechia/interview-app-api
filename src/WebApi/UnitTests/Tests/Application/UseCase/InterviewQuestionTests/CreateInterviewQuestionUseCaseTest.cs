using Application.Repositories;
using Application.UseCases.InterviewQuestion.CreateInterviewQuestion;
using Domain.Models;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Tests.Application.UseCase.InterviewQuestionTests;

public class CreateInterviewQuestionUseCaseTest
{
    private static CreateInterviewQuestionInput Input => new CreateInterviewQuestionInput()
    {
        Title = "test",
        Difficulty = 1,
        Category = "test",
        Content = "test"
    };

    [Fact]
    public async Task Execute_RepositoryNull_Throws()
    {
        var useCase = new CreateInterviewQuestionUseCase(null);

        var execute = async () => await useCase.Execute(Input);

        await Assert.ThrowsAsync<NullReferenceException>(execute);
    }

    [Fact]
    public async Task Execute_PassNull_Throws()
    {
        var useCase = new CreateInterviewQuestionUseCase(It.IsAny<IInterviewQuestionRepository>());

        var execute = async () => await useCase.Execute(null);

        await Assert.ThrowsAsync<ArgumentNullException>(execute);
    }

    [Fact]
    public async Task Execute_PassValidInput_CallsOk()
    {
        var repositoryMock = new Mock<IInterviewQuestionRepository>();
        repositoryMock.Setup(x => x.Create(It.IsAny<InterviewQuestionModel>()).Result).Returns(new InterviewQuestionModel());
        var outputPortMock = new Mock<IOutputPort>();
        var useCase = new CreateInterviewQuestionUseCase(repositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(Input);

        outputPortMock.Verify(x => x.Ok(), Times.Once());
        outputPortMock.Verify(x => x.Invalid(), Times.Never());
        outputPortMock.Verify(x => x.NotFound(), Times.Never());
    }

    [Fact]
    public async Task Execute_PassValidInput_CallsInvalid()
    {
        var repositoryMock = new Mock<IInterviewQuestionRepository>();
        repositoryMock.Setup(x => x.Create(It.IsAny<InterviewQuestionModel>()).Result).Returns(new InterviewQuestionModel());
        var outputPortMock = new Mock<IOutputPort>();
        var useCase = new CreateInterviewQuestionUseCase(repositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(Input);

        outputPortMock.Verify(x => x.Ok(), Times.Never());
        outputPortMock.Verify(x => x.Invalid(), Times.Once());
        outputPortMock.Verify(x => x.NotFound(), Times.Never());
    }
}
