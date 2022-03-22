using Domain.Models;

namespace Application.UseCases.QuestionSet.CreateQuestionSet;

public interface IOutputPort
{
    void Invalid();

    void NotFound();

    void Ok(QuestionSetModel model);
}
