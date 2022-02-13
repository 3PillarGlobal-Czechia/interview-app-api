using System.Threading.Tasks;

namespace Application.UseCases.QuestionList.GetQuestionList;

public interface IGetQuestionListUseCase
{
    Task Execute(GetQuestionListInput input);

    void SetOutputPort(IOutputPort outputPort);
}
