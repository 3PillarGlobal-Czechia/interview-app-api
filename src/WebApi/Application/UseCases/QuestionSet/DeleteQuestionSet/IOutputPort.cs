namespace Application.UseCases.QuestionSet.DeleteQuestionSet;

public interface IOutputPort
{
    void Invalid();

    void NotFound();

    void NoContent();
}

