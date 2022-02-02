namespace WebApi.UseCases.v1.CreateInterviewQuestion;

public record CreateInterviewQuestionRequest
{
    public string Title { get; init; }

    public int? Difficulty { get; init; }

    public string Category { get; init; }

    public string Content { get; init; }
}
