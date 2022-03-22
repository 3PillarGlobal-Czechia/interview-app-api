using System.Threading.Tasks;

namespace Application.UseCases.InterviewQuestion.UpdateInterviewQuestion
{
    public interface IUpdateInterviewQuestionUseCase
    {
        Task Execute(UpdateInterviewQuestionInput input);

        void SetOutputPort(IOutputPort outputPort);
    }
}
