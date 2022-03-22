using System.Threading.Tasks;

namespace Application.UseCases.InterviewQuestion.GetInterviewQuestion;

public interface IGetInterviewQuestionUseCase
{
    Task Execute(GetInterviewQuestionInput input);

    void SetOutputPort(IOutputPort outputPort);
}
