using System.Threading.Tasks;

namespace Application.UseCases.QuestionSet.CreateQuestionSet;

public interface ICreateQuestionSetUseCase
{
    Task Execute(CreateQuestionSetInput input);

    void SetOutputPort(IOutputPort outputPort);
}
