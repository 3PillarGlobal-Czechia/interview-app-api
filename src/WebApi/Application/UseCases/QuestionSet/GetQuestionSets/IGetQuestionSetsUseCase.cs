using System.Threading.Tasks;

namespace Application.UseCases.QuestionSet.GetQuestionSets;

public interface IGetQuestionSetsUseCase
{
    Task Execute(GetQuestionSetsInput input);

    void SetOutputPort(IOutputPort outputPort);
}
