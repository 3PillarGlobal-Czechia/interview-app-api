namespace Application.UseCases.InterviewQuestion.CreateInterviewQuestion;

public interface IOutputPort
{
    void Invalid();

    void NotFound();

    void Ok();
}
