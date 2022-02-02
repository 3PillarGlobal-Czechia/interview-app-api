using System.Collections.Generic;

namespace Application.UseCases.InterviewQuestion.GetInterviewQuestion;

public record GetInterviewQuestionInput
{
    public string Category { get; init; }

    public string Text { get; init; }

    public IEnumerable<int> Difficulties { get; init; }
}
