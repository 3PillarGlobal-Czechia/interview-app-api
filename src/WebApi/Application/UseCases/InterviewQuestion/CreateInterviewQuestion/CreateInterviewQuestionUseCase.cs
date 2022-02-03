using Application.Repositories;
using Domain.Models;
using System;
using System.Threading.Tasks;

namespace Application.UseCases.InterviewQuestion.CreateInterviewQuestion;

public class CreateInterviewQuestionUseCase : ICreateInterviewQuestionUseCase
{
    private IOutputPort _outputPort;

    private readonly IInterviewQuestionRepository _interviewQuestionRepository;

    public CreateInterviewQuestionUseCase(IInterviewQuestionRepository interviewQuestionRepository)
    {
        _outputPort = new CreateInterviewQuestionPresenter();
        _interviewQuestionRepository = interviewQuestionRepository;
    }

    public async Task Execute(CreateInterviewQuestionInput input)
    {
        if (input == null)
        {
            throw new ArgumentNullException(nameof(input));
        }

        await CreateInterviewQuestionInternal(input);
    }

    private async Task CreateInterviewQuestionInternal(CreateInterviewQuestionInput input)
    {
        InterviewQuestionModel model = new()
        {
            Category = input.Category,
            Difficulty = input.Difficulty,
            Title = input.Title,
            Content = input.Content,
            Id = 0
        };

        bool isCreated = await _interviewQuestionRepository.Create(model);

        if (isCreated)
        {
            _outputPort.Ok();
        }
        else
        {
            _outputPort.Invalid();
        }
    }

    public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;
}
