using Application.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCases.InterviewQuestion.GetInterviewQuestion;

public class GetInterviewQuestionUseCase : IGetInterviewQuestionUseCase
{
    private IOutputPort _outputPort;

    private readonly IInterviewQuestionRepository _interviewQuestionRepository;

    public GetInterviewQuestionUseCase(IInterviewQuestionRepository interviewQuestionRepository)
    {
        _outputPort = new GetInterviewQuestionPresenter();
        _interviewQuestionRepository = interviewQuestionRepository;
    }

    public async Task Execute(GetInterviewQuestionInput input)
    {
        if (input == null)
        {
            throw new ArgumentNullException(nameof(input));
        }

        await GetInterviewQuestionInternal(input);
    }

    private async Task GetInterviewQuestionInternal(GetInterviewQuestionInput input)
    {
        var interviewQuestions = await _interviewQuestionRepository.Get(input);

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
