using System.Threading.Tasks;

namespace Application.UseCases.InterviewQuestion.CreateInterviewQuestion
{
    public interface ICreateInterviewQuestionUseCase
    {
        Task Execute(CreateInterviewQuestionInput input);
        void SetOutputPort(IOutputPort outputPort);
    }
}