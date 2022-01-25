using System.Collections.Generic;

namespace Application.UseCases.InterviewQuestion.GetInterviewQuestion;

public record GetInterviewQuestionInput
{
    public string Category { get; set; }

    public string Text { get; set; }

    public IEnumerable<int> Difficulties { get; set; }
}
