using Application.Repositories;
using Application.UseCases.QuestionSet.CreateQuestionSet;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Tests.Application.UseCase.QuestionSetTests;

public class CreateQuestionSetUseCaseTest
{
    private static CreateQuestionSetInput Input => new CreateQuestionSetInput()
    {
        Title = "test",
        Description = "test"
    };

    [Fact]
    public async Task Execute_RepositoryNull_Throws()
    {
        var useCase = new CreateQuestionSetUseCase(null);

        var execute = async () => await useCase.Execute(Input);

        await Assert.ThrowsAsync<NullReferenceException>(execute);
    }

    [Fact]
    public async Task Execute_PassEmpty_Throws()
    {
        var useCase = new CreateQuestionSetUseCase(It.IsAny<IQuestionSetRepository>());

        var execute = async () => await useCase.Execute(It.IsAny<CreateQuestionSetInput>());

        await Assert.ThrowsAsync<ArgumentException>(execute);
    }

    [Fact]
    public async Task Execute_PassValidInput_CallsOk()
    {
        var repositoryMock = new Mock<IQuestionSetRepository>();
        repositoryMock.Setup(x => x.Create(It.IsAny<QuestionSetModel>()).Result).Returns(new QuestionSetModel()
        {
            Title = "test",
            Description = "test"
        });
        repositoryMock.Setup(x => x.AddQuestionsToList(It.IsAny<QuestionSetModel>(), It.IsAny<IEnumerable<int>>()).Result)
                      .Returns(true);
        var outputPortMock = new Mock<IOutputPort>();
        var useCase = new CreateQuestionSetUseCase(repositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(Input);

        outputPortMock.Verify(x => x.Ok(It.IsAny<QuestionSetModel>()), Times.Once());
        outputPortMock.Verify(x => x.Invalid(), Times.Never());
        outputPortMock.Verify(x => x.NotFound(), Times.Never());
    }

    [Fact]
    public async Task Execute_PassValidInput_CallsInvalid()
    {
        var repositoryMock = new Mock<IQuestionSetRepository>();
        repositoryMock.Setup(x => x.Create(It.IsAny<QuestionSetModel>()).Result).Returns(new QuestionSetModel());
        var outputPortMock = new Mock<IOutputPort>();
        var useCase = new CreateQuestionSetUseCase(repositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(Input);

        outputPortMock.Verify(x => x.Ok(It.IsAny<QuestionSetModel>()), Times.Never());
        outputPortMock.Verify(x => x.Invalid(), Times.Once());
        outputPortMock.Verify(x => x.NotFound(), Times.Never());
    }
}
