namespace Application.UseCases.QuestionSet.UpdateQuestionSet;

public interface IOutputPort
{
    void Invalid();

    void NotFound();

    void Ok();
}
