using Application.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCases.QuestionList.GetQuestionList;

public class GetQuestionListUseCase : IGetQuestionListUseCase
{
    private IOutputPort _outputPort;

    private IQuestionListRepository _questionListRepository;

    public GetQuestionListUseCase(IQuestionListRepository questionListRepository)
    {
        _outputPort = new GetQuestionListPresenter();
        _questionListRepository = questionListRepository;
    }

    public async Task Execute(GetQuestionListInput input)
    {
        if (input == null)
        {
            throw new ArgumentNullException(nameof(input));
        }

        await GetQuestionListInternal(input);
    }

    public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;

    private async Task GetQuestionListInternal(GetQuestionListInput input)
    {
        var questionLists = await _questionListRepository.Get(input);

        if (questionLists != null && questionLists.Any())
        {
            _outputPort.Ok(questionLists);
        }
        else
        {
            _outputPort.NotFound();
        }
    }
}
