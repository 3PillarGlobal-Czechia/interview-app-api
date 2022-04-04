namespace Application.UseCases.Question.DeleteInterviewQuestion;

public interface IOutputPort
{
    void Invalid();

    void NotFound();

    void NoContent();
}
