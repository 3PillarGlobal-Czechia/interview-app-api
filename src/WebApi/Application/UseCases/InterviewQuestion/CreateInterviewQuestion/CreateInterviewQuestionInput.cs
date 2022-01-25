namespace Application.UseCases.InterviewQuestion.CreateInterviewQuestion;

public record CreateInterviewQuestionInput
{
    public string Title { get; set; }

    public int? Difficulty { get; set; }

    public string Category { get; set; }

    public string Content { get; set; }
}
