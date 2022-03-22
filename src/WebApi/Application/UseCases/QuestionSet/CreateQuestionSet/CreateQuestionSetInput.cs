using System.Collections.Generic;

namespace Application.UseCases.QuestionSet.CreateQuestionList;

public record CreateQuestionSetInput
{
    public string Title { get; init; }

    public string Description { get; init; }

    public IEnumerable<int> InterviewQuestionIds { get; init; }
}
