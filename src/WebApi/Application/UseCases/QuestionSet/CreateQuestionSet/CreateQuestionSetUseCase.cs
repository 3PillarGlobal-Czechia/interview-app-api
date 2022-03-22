using Application.Repositories;
using Domain.Models;
using System;
using System.Threading.Tasks;

namespace Application.UseCases.QuestionSet.CreateQuestionSet;

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

        await CreateQuestionSetInternal(input);
    }

    public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;

    private async Task CreateQuestionSetInternal(CreateQuestionSetInput input)
    {
        var questionSetModel = new QuestionSetModel
        {
            Title = input.Title,
            Description = input.Description
        };

        questionSetModel = await _questionSetRepository.Create(questionSetModel);

        bool isCreated = await _questionSetRepository.AddQuestionsToList(questionSetModel, input.InterviewQuestionIds);

        if (!isCreated)
        {
            _outputPort.Invalid();
            return;
        }

        _outputPort.Ok(questionSetModel);
    }
}
