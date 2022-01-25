using Application.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCases.InterviewQuestion.GetInterviewQuestion;

public class GetInterviewQuestionUseCase : IGetInterviewQuestionUseCase
{
    private IOutputPort _outputPort;

    private IInterviewQuestionRepository _interviewQuestionRepository;

    public GetInterviewQuestionUseCase(IInterviewQuestionRepository interviewQuestionRepository)
    {
        _outputPort = new GetInterviewQuestionPresenter();
        _interviewQuestionRepository = interviewQuestionRepository;
    }

    public async Task Execute(GetInterviewQuestionInput input)
    {
        var interviewQuestions = await _interviewQuestionRepository.Get(input);

        if (interviewQuestions == null || !interviewQuestions.Any())
        {
            _outputPort.NotFound();
        }

        _outputPort.Ok(interviewQuestions);
    }

    public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;
}
