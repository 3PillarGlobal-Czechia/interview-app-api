using System.Collections.Generic;

namespace WebApi.UseCases.v1.GetInterviewQuestion;

public record GetInterviewQuestionRequest
{
    public string Category { get; set; }

    public string Text { get; set; }

    public IEnumerable<int> Difficulties { get; set; }
}
