using Application.UseCases.Question.CreateInterviewQuestion;
using System.Threading.Tasks;

namespace Application.UseCases.InterviewQuestion.CreateInterviewQuestion
{
    public interface ICreateQuestionUseCase
    {
        Task Execute(CreateQuestionInput input);

        void SetOutputPort(IOutputPort outputPort);
    }
}
