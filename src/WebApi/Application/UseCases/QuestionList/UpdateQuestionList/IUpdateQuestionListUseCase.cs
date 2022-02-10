using System.Threading.Tasks;

namespace Application.UseCases.QuestionList.UpdateQuestionList
{
    public interface IUpdateQuestionListUseCase
    {
        Task Execute(UpdateQuestionListInput input);
        void SetOutputPort(IOutputPort outputPort);
    }
}