using System.Threading.Tasks;

namespace Application.UseCases.QuestionSet.UpdateQuestionSet;

public interface IUpdateQuestionSetUseCase
{
    Task Execute(UpdateQuestionSetInput input);

    void SetOutputPort(IOutputPort outputPort);
}
