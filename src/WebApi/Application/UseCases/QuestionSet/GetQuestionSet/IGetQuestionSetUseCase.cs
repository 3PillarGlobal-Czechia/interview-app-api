using System.Threading.Tasks;

namespace Application.UseCases.QuestionSet.GetQuestionList;

public interface IGetQuestionSetUseCase
{
    Task Execute(GetQuestionSetInput input);

    void SetOutputPort(IOutputPort outputPort);
}
