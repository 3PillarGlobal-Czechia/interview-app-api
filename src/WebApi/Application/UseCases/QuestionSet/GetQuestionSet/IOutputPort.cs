using Domain.Models;

namespace Application.UseCases.QuestionSet.GetQuestionList;

public interface IOutputPort
{
    void Invalid();

    void NotFound();

    void Ok(QuestionSetModel questionSet);
}
