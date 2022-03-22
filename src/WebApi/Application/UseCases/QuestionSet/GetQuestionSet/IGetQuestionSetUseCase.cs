using System.Threading.Tasks;

namespace Application.UseCases.QuestionList.GetQuestionList;

public interface IGetQuestionSetUseCase
{
    Task Execute(GetQuestionSetInput input);

    void SetOutputPort(IOutputPort outputPort);
}
