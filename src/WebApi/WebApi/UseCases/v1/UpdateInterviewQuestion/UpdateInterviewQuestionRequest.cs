namespace WebApi.UseCases.v1.UpdateInterviewQuestion;

public record UpdateInterviewQuestionRequest
{
    public int Id { get; init; }

    public string Title { get; init; }

    public int? Difficulty { get; init; }

    public string Category { get; init; }

    public string Content { get; init; }
}
