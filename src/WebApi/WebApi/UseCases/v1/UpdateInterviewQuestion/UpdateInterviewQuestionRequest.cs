namespace WebApi.UseCases.v1.UpdateInterviewQuestion;

public record UpdateInterviewQuestionRequest
{
    public int Id { get; set; }

    public string Title { get; set; }

    public int? Difficulty { get; set; }

    public string Category { get; set; }

    public string Content { get; set; }
}
