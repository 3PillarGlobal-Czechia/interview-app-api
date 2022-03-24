using Application.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCases.InterviewQuestion.GetInterviewQuestion;

public class GetInterviewQuestionUseCase : IGetInterviewQuestionUseCase
{
    private IOutputPort _outputPort;

    private readonly IQuestionRepository _questionRepository;

    public GetInterviewQuestionUseCase(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task Execute(GetInterviewQuestionInput input)
    {
        var interviewQuestions = await _questionRepository.Get(input);

        if (interviewQuestions != null && interviewQuestions.Any())
        {
            _outputPort.Ok(interviewQuestions);
        }
        else
        {
            _outputPort.NotFound();
        }
    }

    public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;
}
