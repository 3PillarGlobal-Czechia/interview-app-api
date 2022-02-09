namespace Application.UseCases.QuestionList.CreateQuestionList;

public interface IOutputPort
{
    void Invalid();

    void NotFound();

    void Ok();
}
