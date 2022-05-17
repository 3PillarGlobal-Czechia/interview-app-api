
using System;
using System.Threading.Tasks;
using Application.Repositories;
using Application.UseCases.User.CreateUser;
using Domain.Models;
using Moq;
using Xunit;

namespace UnitTests.Tests.Application.UseCase.Account;

public class Login
{
    private static EnsureCreatedUserInput Input => new EnsureCreatedUserInput()
    {
        GoogleId = "27",
        FirstName = "Jan",
        LastName = "Novak",
        UserName = "JanNovak",
        Email = "jan.novak@3pillarglobal.com",
        ImageURL = ""
    };

    [Fact]
    public async Task Execute_RepositoryNull_Throws()
    {
        var useCase = new EnsureCreatedUserUseCase(null);

        var execute = async () => await useCase.Execute(Input);

        await Assert.ThrowsAsync<NullReferenceException>(execute);
    }

    [Fact]
    public async Task Execute_PassEmpty_Throws()
    {
        var useCase = new EnsureCreatedUserUseCase(It.IsAny<IUserRepository>());

        var execute = async () => await useCase.Execute(It.IsAny<EnsureCreatedUserInput>());

        await Assert.ThrowsAsync<NullReferenceException>(execute);
    }

    [Fact]
    public async Task Execute_PassValidInput_CallsOk()
    {
        var repositoryMock = new Mock<IUserRepository>();
        repositoryMock.Setup(x => x.Create(It.IsAny<UserModel>()).Result).Returns(new UserModel());
        var outputPortMock = new Mock<IOutputPort>();
        var useCase = new EnsureCreatedUserUseCase(repositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(Input);

        outputPortMock.Verify(x => x.Ok(), Times.Once());
        outputPortMock.Verify(x => x.Invalid(), Times.Never());
    }

    [Fact]
    public async Task Execute_PassValidInput_CallsInvalid()
    {
        var repositoryMock = new Mock<IUserRepository>();
        repositoryMock.Setup(x => x.Create(It.IsAny<UserModel>())).ReturnsAsync((UserModel)null);
        var outputPortMock = new Mock<IOutputPort>();
        var useCase = new EnsureCreatedUserUseCase(repositoryMock.Object);
        useCase.SetOutputPort(outputPortMock.Object);

        await useCase.Execute(Input);

        outputPortMock.Verify(x => x.Ok(), Times.Never());
        outputPortMock.Verify(x => x.Invalid(), Times.Once());
    }
}
