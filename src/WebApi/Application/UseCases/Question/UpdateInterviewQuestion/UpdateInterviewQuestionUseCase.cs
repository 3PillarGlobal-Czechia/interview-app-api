using Application.Repositories;
using System;
using System.Threading.Tasks;

namespace Application.UseCases.InterviewQuestion.UpdateInterviewQuestion;

public class UpdateInterviewQuestionUseCase : IUpdateInterviewQuestionUseCase
{
    private IOutputPort _outputPort;

    private readonly IQuestionRepository _questionRepository;

    public UpdateInterviewQuestionUseCase(IQuestionRepository interviewQuestionRepository)
    {
        _questionRepository = interviewQuestionRepository;
    }

    public async Task Execute(UpdateInterviewQuestionInput input)
    {
        var existingInterviewQuestion = await _questionRepository.GetById(input.Id);

        if (existingInterviewQuestion == null)
        {
            _outputPort.NotFound();
            return;
        }

        existingInterviewQuestion.Difficulty = input.Difficulty;
        existingInterviewQuestion.Title = input.Title;
        existingInterviewQuestion.Content = input.Content;
        existingInterviewQuestion.Category = input.Category;

        bool isUpdated = await _questionRepository.Update(existingInterviewQuestion);

        if (isUpdated)
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
