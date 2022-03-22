namespace Application.UseCases.QuestionList.UpdateQuestionList;

public interface IOutputPort
{
    void Invalid();

    void NotFound();

    void Ok();
}
