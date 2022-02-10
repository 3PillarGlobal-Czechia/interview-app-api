using Application.Repositories;
using Domain.Models;
using System;
using System.Threading.Tasks;

namespace Application.UseCases.QuestionList.CreateQuestionList;

public class CreateQuestionListUseCase : ICreateQuestionListUseCase
{
    private IOutputPort _outputPort;

    private readonly IQuestionListRepository _questionListRepository;

    public CreateQuestionListUseCase(IQuestionListRepository questionListRepository)
    {
        _outputPort = new CreateQuestionListPresenter();
        _questionListRepository = questionListRepository;
    }

    public async Task Execute(CreateQuestionListInput input)
    {
        if (input == null)
        {
            throw new ArgumentNullException(nameof(input));
        }

        if (input.Title == null)
        {
            throw new ArgumentException("Please provide a title for a new question list!", nameof(input));
        }

        await CreateQuestionListInternal(input);
    }

    public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;

    private async Task CreateQuestionListInternal(CreateQuestionListInput input)
    {
        var questionListModel = new QuestionListModel()
        {
            Title = input.Title,
            Description = input.Description
        };

        questionListModel = await _questionListRepository.Create(questionListModel);

        bool isCreated = await _questionListRepository.AddQuestionsToList(questionListModel, input.InterviewQuestionIds);

        if (isCreated)
        {
            _outputPort.Ok();
        }
        else
        {
            _outputPort.Invalid();
        }
    }
}
