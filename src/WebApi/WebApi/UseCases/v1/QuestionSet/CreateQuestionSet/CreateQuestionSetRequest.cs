using System.Collections.Generic;

namespace WebApi.UseCases.v1.QuestionSet.CreateQuestionSet;

public record CreateQuestionSetRequest
{
    public string Title { get; set; }

    public string Description { get; set; }

    public IEnumerable<int> InterviewQuestionIds { get; set; }
}
