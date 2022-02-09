using System.Collections.Generic;

namespace WebApi.UseCases.v1.QuestionList.CreateQuestionList;

public record CreateQuestionListRequest
{
    public string Title { get; set; }

    public string Description { get; set; }

    public IEnumerable<int> InterviewQuestionIds { get; set; }
}
