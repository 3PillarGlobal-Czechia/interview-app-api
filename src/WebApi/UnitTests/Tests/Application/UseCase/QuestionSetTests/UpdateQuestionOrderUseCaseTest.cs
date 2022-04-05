using Application.Repositories;
using Application.UseCases.QuestionSet.UpdateQuestionOrder;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Tests.Application.UseCase.QuestionSetTests;

public class UpdateQuestionOrderUseCaseTest
{
    private static UpdateQuestionOrderInput Input => new()
    {
        QuestionSetId = 1,
        OrderedQuestionIds = new[] { 1, 2, 3, 4 }
    };

    private static readonly QuestionSetModel Model = new()
    {
        Id = 1,
        Title = "test",
        Description = "test",
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now,
    };

    [Fact]
    public async Task Execute_RepositoryNull_Throws()
    {
        var useCase = new UpdateQuestionOrderUseCase(null);

        var execute = async () => await useCase.Execute(Input);

        await Assert.ThrowsAsync<NullReferenceException>(execute);
    }

    [Fact]
    public async Task Execute_PassEmpty_Throws()
    {
        var useCase = new UpdateQuestionOrderUseCase(It.IsAny<IQuestionSetRepository>());

        var execute = async () => await useCase.Execute(It.IsAny<UpdateQuestionOrderInput>());

        await Assert.ThrowsAsync<NullReferenceException>(execute);
    }

    [Fact]
    public async Task Execute_PassValidInput_CallsOk()
    {
        var repositoryMock = new Mock<IQuestionSetRepository>();
        repositoryMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(Model);
        repositoryMock.Setup(x => x.UpdateQuestionOrder(It.IsAny<int>(), It.IsAny<IList<int>>())).ReturnsAsync(true);
        var outputPortMock = new Mock<IOutputPort>();
        var useCase = new UpdateQuestionOrderUseCase(repositoryMock.Object);
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
        repositoryMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(Model);
        repositoryMock.Setup(x => x.UpdateQuestionOrder(It.IsAny<int>(), It.IsAny<IList<int>>())).ReturnsAsync(false);
        var outputPortMock = new Mock<IOutputPort>();
        var useCase = new UpdateQuestionOrderUseCase(repositoryMock.Object);
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
        var useCase = new UpdateQuestionOrderUseCase(repositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(Input);

        outputPortMock.Verify(x => x.Ok(), Times.Never());
        outputPortMock.Verify(x => x.Invalid(), Times.Never());
        outputPortMock.Verify(x => x.NotFound(), Times.Once());
    }
}
