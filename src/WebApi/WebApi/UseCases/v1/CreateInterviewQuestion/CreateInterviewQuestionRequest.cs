namespace WebApi.UseCases.v1.CreateInterviewQuestion;

public record CreateInterviewQuestionRequest
{
    public string Title { get; set; }

    public int? Difficulty { get; set; }

    public string Category { get; set; }

    public string Content { get; set; }
}
