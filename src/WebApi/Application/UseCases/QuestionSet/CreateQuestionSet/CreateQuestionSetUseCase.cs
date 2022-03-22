using Application.Repositories;
using Domain.Models;
using System;
using System.Threading.Tasks;

namespace Application.UseCases.QuestionSet.CreateQuestionList;

public class CreateQuestionSetUseCase : ICreateQuestionSetUseCase
{
    private IOutputPort _outputPort;

    private readonly IQuestionSetRepository _questionSetRepository;

    public CreateQuestionSetUseCase(IQuestionSetRepository questionSetRepository)
    {
        _questionSetRepository = questionSetRepository;
    }

    public async Task Execute(CreateQuestionSetInput input)
    {
        if (input is null)
        {
            throw new ArgumentNullException(nameof(input));
        }

        if (input.Title is null)
        {
            throw new ArgumentException("Please provide a title for a new question list!", nameof(input));
        }

        await CreateQuestionListInternal(input);
    }

    public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;

    private async Task CreateQuestionListInternal(CreateQuestionSetInput input)
    {
        var questionListModel = new QuestionSetModel
        {
            Title = input.Title,
            Description = input.Description
        };

        questionListModel = await _questionSetRepository.Create(questionListModel);

        bool isCreated = await _questionSetRepository.AddQuestionsToList(questionListModel, input.InterviewQuestionIds);

        if (!isCreated)
        {
            _outputPort.Invalid();
            return;
        }

        _outputPort.Ok(questionListModel);
    }
}
