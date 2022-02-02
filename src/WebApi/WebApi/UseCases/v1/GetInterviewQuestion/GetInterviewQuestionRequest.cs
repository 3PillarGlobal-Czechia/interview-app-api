using System.Collections.Generic;

namespace WebApi.UseCases.v1.GetInterviewQuestion;

public record GetInterviewQuestionRequest
{
    public string Category { get; init; }

    public string Text { get; init; }

    public IEnumerable<int> Difficulties { get; init; }
}
