namespace Application.UseCases.InterviewQuestion.UpdateInterviewQuestion;

public record UpdateInterviewQuestionInput
{
    public int Id { get; set; }

    public string Title { get; set; }

    public int? Difficulty { get; set; }

    public string Category { get; set; }

    public string Content { get; set; }
}
