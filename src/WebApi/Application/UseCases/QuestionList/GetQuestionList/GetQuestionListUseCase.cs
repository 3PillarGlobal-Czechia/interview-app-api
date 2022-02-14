using Application.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCases.QuestionList.GetQuestionList;

public class GetQuestionListUseCase : IGetQuestionListUseCase
{
    private IOutputPort _outputPort;

    private readonly IQuestionListRepository _questionListRepository;

    public GetQuestionListUseCase(IQuestionListRepository questionListRepository)
    {
        _questionListRepository = questionListRepository;
    }

    public async Task Execute(GetQuestionListInput input)
    {
        if (input is null)
        {
            throw new ArgumentNullException(nameof(input));
        }

        await GetQuestionListInternal(input);
    }

    public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;

    private async Task GetQuestionListInternal(GetQuestionListInput input)
    {
        var questionLists = await _questionListRepository.Get(input);

        if (questionLists is null || !questionLists.Any())
        {
            _outputPort.NotFound();
            return;
        }

        _outputPort.Ok(questionLists);
    }
}
