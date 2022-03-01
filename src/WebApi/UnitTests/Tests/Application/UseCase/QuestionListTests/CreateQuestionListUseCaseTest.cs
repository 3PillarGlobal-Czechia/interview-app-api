using Application.Repositories;
using Application.UseCases.QuestionList.CreateQuestionList;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Tests.Application.UseCase.QuestionListTests;

public class CreateQuestionListUseCaseTest
{
    private static CreateQuestionListInput Input => new CreateQuestionListInput()
    {
        Title = "test",
        Description = "test"
    };

    [Fact]
    public async Task Execute_RepositoryNull_Throws()
    {
        var useCase = new CreateQuestionListUseCase(null);

        var execute = async () => await useCase.Execute(Input);

        await Assert.ThrowsAsync<NullReferenceException>(execute);
    }

    [Fact]
    public async Task Execute_PassNull_Throws()
    {
        var useCase = new CreateQuestionListUseCase(It.IsAny<IQuestionListRepository>());

        var execute = async () => await useCase.Execute(null);

        await Assert.ThrowsAsync<ArgumentNullException>(execute);
    }

    [Fact]
    public async Task Execute_PassValidInput_CallsOk()
    {
        var repositoryMock = new Mock<IQuestionListRepository>();
        repositoryMock.Setup(x => x.Create(It.IsAny<QuestionListModel>()).Result).Returns(new QuestionListModel()
        {
            Title = "test",
            Description = "test"
        });
        repositoryMock.Setup(x => x.AddQuestionsToList(It.IsAny<QuestionListModel>(), It.IsAny<IEnumerable<int>>()).Result)
                      .Returns(true);
        var outputPortMock = new Mock<IOutputPort>();
        var useCase = new CreateQuestionListUseCase(repositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(Input);

        outputPortMock.Verify(x => x.Ok(It.IsAny<QuestionListModel>()), Times.Once());
        outputPortMock.Verify(x => x.Invalid(), Times.Never());
        outputPortMock.Verify(x => x.NotFound(), Times.Never());
    }

    [Fact]
    public async Task Execute_PassValidInput_CallsInvalid()
    {
        var repositoryMock = new Mock<IQuestionListRepository>();
        repositoryMock.Setup(x => x.Create(It.IsAny<QuestionListModel>()).Result).Returns(new QuestionListModel());
        var outputPortMock = new Mock<IOutputPort>();
        var useCase = new CreateQuestionListUseCase(repositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(Input);

        outputPortMock.Verify(x => x.Ok(It.IsAny<QuestionListModel>()), Times.Never());
        outputPortMock.Verify(x => x.Invalid(), Times.Once());
        outputPortMock.Verify(x => x.NotFound(), Times.Never());
    }
}
