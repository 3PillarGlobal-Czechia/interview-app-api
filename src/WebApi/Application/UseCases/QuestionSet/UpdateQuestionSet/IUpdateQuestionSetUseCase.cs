using System.Threading.Tasks;

namespace Application.UseCases.QuestionList.UpdateQuestionList;

public interface IUpdateQuestionSetUseCase
{
    Task Execute(UpdateQuestionSetInput input);

    void SetOutputPort(IOutputPort outputPort);
}
