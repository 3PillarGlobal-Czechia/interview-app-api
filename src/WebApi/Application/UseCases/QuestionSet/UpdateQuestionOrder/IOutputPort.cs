namespace Application.UseCases.QuestionSet.UpdateQuestionOrder;

public interface IOutputPort
{
    void Invalid();

    void NotFound();

    void Ok();
}
