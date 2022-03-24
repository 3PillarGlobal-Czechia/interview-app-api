using Application.Repositories;
using Domain.Models.Agreggates;
using System.Threading.Tasks;

namespace Application.UseCases.QuestionSet.GetQuestionSet;

public class GetQuestionSetUseCase : IGetQuestionSetUseCase
{
    private IOutputPort _outputPort;

    private readonly IQuestionSetRepository _questionSetRepository;
    private readonly IQuestionRepository _questionRepository;

    public GetQuestionSetUseCase(IQuestionSetRepository questionSetRepository, IQuestionRepository questionRepository)
    {
        _questionSetRepository = questionSetRepository;
        _questionRepository = questionRepository;
    }

    public async Task Execute(GetQuestionSetInput input)
    {
        var questionSet = await _questionSetRepository.GetById(input.Id);

        if (questionSet is null)
        {
            _outputPort.NotFound();
            return;
        }

        var questions = await _questionRepository.GetQuestionsBySetId(input.Id);

        var response = new QuestionSetDetail
        {
            QuestionSet = questionSet,
            Questions = questions
        };

        _outputPort.Ok(response);
    }

    public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;
}
