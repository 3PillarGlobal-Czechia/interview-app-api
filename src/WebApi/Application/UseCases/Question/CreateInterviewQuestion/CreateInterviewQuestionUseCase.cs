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
        _interviewQuestionRepository = interviewQuestionRepository;
    }

    public async Task Execute(CreateInterviewQuestionInput input)
    {
        QuestionModel model = new()
        {
            Category = input.Category,
            Difficulty = input.Difficulty,
            Title = input.Title,
            Content = input.Content,
            Id = 0
        };

        model = await _interviewQuestionRepository.Create(model);

        if (model is null)
        {
            _outputPort.Invalid();
            return;
        }

        _outputPort.Ok();
    }

    public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;
}
