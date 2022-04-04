using System.Threading.Tasks;

namespace Application.UseCases.Question.DeleteInterviewQuestion;
public interface IDeleteQuestionUseCase
{
    Task Execute(DeleteQuestionInput input);

    void SetOutputPort(IOutputPort outputPort);
}
