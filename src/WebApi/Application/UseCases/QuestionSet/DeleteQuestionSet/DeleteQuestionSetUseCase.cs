using Application.Repositories;
using System.Threading.Tasks;

namespace Application.UseCases.QuestionSet.DeleteQuestionSet;
public class DeleteQuestionSetUseCase : IDeleteQuestionSetUseCase
{

    private IOutputPort _outputPort;

    private readonly IQuestionSetRepository _questionSetRepository;

    public DeleteQuestionSetUseCase(IQuestionSetRepository questionSetRepository)
    {
        _questionSetRepository = questionSetRepository;
    }

    public async Task Execute(DeleteQuestionSetInput input)
    {
        var questionSet = await _questionSetRepository.GetById(input.Id);

        if (questionSet is null)
        {
            _outputPort.NotFound();
            return;
        }

        var isDeleted = await _questionSetRepository.Delete(input.Id);
            
        if (!isDeleted)
        {
            _outputPort.Invalid();
            return;
        }

        _outputPort.NoContent();
    }

    public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;
}