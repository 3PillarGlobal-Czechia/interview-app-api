using System.Threading.Tasks;

namespace Application.UseCases.QuestionSet.DeleteQuestionSet;

public interface IDeleteQuestionSetUseCase
{
    Task Execute(DeleteQuestionSetInput input);

    void SetOutputPort(IOutputPort outputPort);
}
