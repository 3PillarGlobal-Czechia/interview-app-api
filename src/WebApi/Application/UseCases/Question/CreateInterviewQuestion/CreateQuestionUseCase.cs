using Application.Repositories;
using Application.UseCases.Question.CreateInterviewQuestion;
using Domain.Models;
using System;
using System.Threading.Tasks;

namespace Application.UseCases.InterviewQuestion.CreateInterviewQuestion;

public class CreateQuestionUseCase : ICreateQuestionUseCase
{
    private IOutputPort _outputPort;

    private readonly IQuestionRepository _questionRepository;

    public CreateQuestionUseCase(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task Execute(CreateQuestionInput input)
    {
        QuestionModel model = new()
        {
            Category = input.Category,
            Difficulty = input.Difficulty,
            Title = input.Title,
            Content = input.Content,
            Id = 0
        };

        model = await _questionRepository.Create(model);

        if (model is null)
        {
            _outputPort.Invalid();
            return;
        }

        _outputPort.Ok();
    }

    public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;
}
