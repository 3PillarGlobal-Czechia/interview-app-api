using Domain.Models.Agreggates;

namespace Application.UseCases.QuestionSet.GetQuestionSet;

public interface IOutputPort
{
    void Invalid();

    void NotFound();

    void Ok(QuestionSetDetail questionSet);
}
