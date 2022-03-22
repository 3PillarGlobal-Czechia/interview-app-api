using Domain.Models;

namespace Application.UseCases.QuestionSet.CreateQuestionList;

public interface IOutputPort
{
    void Invalid();

    void NotFound();

    void Ok(QuestionSetModel model);
}
