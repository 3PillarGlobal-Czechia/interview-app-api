using Application.Repositories;
using System.Threading.Tasks;

namespace Application.UseCases.Question.DeleteInterviewQuestion
{
    public class DeleteQuestionUseCase : IDeleteQuestionUseCase
    {

        private IOutputPort _outputPort;

        private readonly IQuestionRepository _questionRepository;

        public DeleteQuestionUseCase(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task Execute(DeleteQuestionInput input)
        {
            var question = await _questionRepository.GetById(input.Id);

            if (question is null)
            {
                _outputPort.NotFound();
                return;
            }

            var isDeleted = await _questionRepository.Delete(input.Id);

            if (!isDeleted)
            {
                _outputPort.Invalid();
                return;
            }

            _outputPort.NoContent();
        }

        public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;
    }
}
