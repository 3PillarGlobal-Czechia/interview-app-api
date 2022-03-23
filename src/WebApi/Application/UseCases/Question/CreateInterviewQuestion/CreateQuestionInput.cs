namespace Application.UseCases.Question.CreateInterviewQuestion;

public readonly struct CreateQuestionInput
{
    public string Title { get; init; }

    public int? Difficulty { get; init; }

    public string Category { get; init; }

    public string Content { get; init; }
}
