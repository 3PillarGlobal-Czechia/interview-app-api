using Domain.Models;

namespace Application.UseCases.QuestionSet.GetQuestionSet;

public interface IOutputPort
{
    void Invalid();

    void NotFound();

    void Ok(QuestionSetModel questionSet);
}
