using Application.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCases.QuestionSet.GetQuestionSets;

public class GetQuestionSetsUseCase : IGetQuestionSetsUseCase
{
    private IOutputPort _outputPort;

    private readonly IQuestionSetRepository _questionListRepository;

    public GetQuestionSetsUseCase(IQuestionSetRepository questionListRepository)
    {
        _questionListRepository = questionListRepository;
    }

    public async Task Execute(GetQuestionSetsInput input)
    {
        if (input is null)
        {
            throw new ArgumentNullException(nameof(input));
        }

        await GetQuestionListInternal(input);
    }

    public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;

    private async Task GetQuestionListInternal(GetQuestionSetsInput input)
    {
        // TODO: implement filtering in separate method
        var questionSets = await _questionListRepository.GetAll();

        // TODO: remap model to QuestionSetItem
        if (questionSets is null || !questionSets.Any())
        {
            _outputPort.NotFound();
            return;
        }

        _outputPort.Ok(questionSets);
    }
}
