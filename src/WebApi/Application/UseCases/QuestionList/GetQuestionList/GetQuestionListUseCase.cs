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
        var questionLists = await _questionListRepository.Get(input);

        if (questionLists is null || !questionLists.Any())
        {
            _outputPort.NotFound();
            return;
        }

        _outputPort.Ok(questionLists);
    }

    public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;
}
