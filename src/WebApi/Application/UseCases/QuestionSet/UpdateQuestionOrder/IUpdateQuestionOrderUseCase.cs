using System.Threading.Tasks;

namespace Application.UseCases.QuestionSet.UpdateQuestionOrder;

public interface IUpdateQuestionOrderUseCase
{
    Task Execute(UpdateQuestionOrderInput input);

    void SetOutputPort(IOutputPort outputPort);
}
