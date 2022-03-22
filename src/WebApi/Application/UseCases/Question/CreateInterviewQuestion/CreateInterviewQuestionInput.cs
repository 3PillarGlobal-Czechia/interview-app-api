namespace Application.UseCases.InterviewQuestion.CreateInterviewQuestion;

public record CreateInterviewQuestionInput
{
    public string Title { get; init; }

    public int? Difficulty { get; init; }

    public string Category { get; init; }

    public string Content { get; init; }
}
