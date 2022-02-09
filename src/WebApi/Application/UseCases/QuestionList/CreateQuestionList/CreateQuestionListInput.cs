using System.Collections.Generic;

namespace Application.UseCases.QuestionList.CreateQuestionList;

public record CreateQuestionListInput
{
    public string Title { get; set; }

    public string Description { get; set; }

    public IEnumerable<int> InterviewQuestionIds { get; set; }
}
