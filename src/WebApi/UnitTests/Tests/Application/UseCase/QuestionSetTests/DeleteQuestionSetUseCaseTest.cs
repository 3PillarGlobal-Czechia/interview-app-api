using Application.Repositories;
using Application.UseCases.QuestionSet.DeleteQuestionSet;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Tests.Application.UseCase.QuestionSetTests
{
    public class DeleteQuestionSetUseCaseTest
    {
        private static DeleteQuestionSetInput Input => new DeleteQuestionSetInput()
        {
            Id = 1
        };

        [Fact]
        public async Task Execute_RepositoryNull_Throws()
        {
            var useCase = new DeleteQuestionSetUseCase(null);

            var execute = async () => await useCase.Execute(Input);

            await Assert.ThrowsAsync<NullReferenceException>(execute);
        }

        [Fact]
        public async Task Execute_PassEmpty_Throws()
        {
            var useCase = new DeleteQuestionSetUseCase(It.IsAny<IQuestionSetRepository>());

            var execute = async () => await useCase.Execute(It.IsAny<DeleteQuestionSetInput>());

            await Assert.ThrowsAsync<NullReferenceException>(execute);
        }

        [Fact]
        public async Task Execute_PassValidInput_CallsNoContent()
        {
            var repositoryMock = new Mock<IQuestionSetRepository>();
            repositoryMock.Setup(x => x.GetById(1).Result).Returns(new QuestionSetModel());
            repositoryMock.Setup(x => x.Delete(1).Result).Returns(true);
            repositoryMock.Setup(x => x.RemoveQuestionsFromList(1, It.IsAny<IEnumerable<int>>()).Result).Returns(true);
            
            var outputPortMock = new Mock<IOutputPort>();
            var useCase = new DeleteQuestionSetUseCase(repositoryMock.Object);
            useCase.SetOutputPort(outputPortMock.Object);

            await useCase.Execute(Input);

            outputPortMock.Verify(x => x.NoContent(), Times.Once());
            outputPortMock.Verify(x => x.Invalid(), Times.Never());
            outputPortMock.Verify(x => x.NotFound(), Times.Never());
        }

        [Fact]
        public async Task Execute_PassValidInput_CallsInvalid()
        {
            var repositoryMock = new Mock<IQuestionSetRepository>();
            repositoryMock.Setup(x => x.GetById(1).Result).Returns(new QuestionSetModel());
            repositoryMock.Setup(x => x.Delete(It.IsAny<QuestionSetModel>()).Result).Returns(false);
            var outputPortMock = new Mock<IOutputPort>();
            var useCase = new DeleteQuestionSetUseCase(repositoryMock.Object);
            useCase.SetOutputPort(outputPortMock.Object);

            await useCase.Execute(Input);

            outputPortMock.Verify(x => x.NoContent(), Times.Never());
            outputPortMock.Verify(x => x.Invalid(), Times.Once());
            outputPortMock.Verify(x => x.NotFound(), Times.Never());
        }
    }
}
