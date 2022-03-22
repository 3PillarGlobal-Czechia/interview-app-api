namespace Application.UseCases.InterviewQuestion.UpdateInterviewQuestion;

public interface IOutputPort
{
    void Invalid();

    void NotFound();

    void Ok();
}
