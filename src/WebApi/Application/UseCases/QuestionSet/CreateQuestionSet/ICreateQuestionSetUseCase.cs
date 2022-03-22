using System.Threading.Tasks;

namespace Application.UseCases.QuestionList.CreateQuestionList;

public interface ICreateQuestionSetUseCase
{
    Task Execute(CreateQuestionSetInput input);

    void SetOutputPort(IOutputPort outputPort);
}
