using System.Threading.Tasks;

namespace Application.UseCases.QuestionList.CreateQuestionList
{
    public interface ICreateQuestionListUseCase
    {
        Task Execute(CreateQuestionListInput input);
        void SetOutputPort(IOutputPort outputPort);
    }
}