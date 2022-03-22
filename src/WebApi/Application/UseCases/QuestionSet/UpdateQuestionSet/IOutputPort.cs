namespace Application.UseCases.QuestionSet.UpdateQuestionList;

public interface IOutputPort
{
    void Invalid();

    void NotFound();

    void Ok();
}
