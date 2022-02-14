using System.Collections.Generic;

namespace Application.UseCases.QuestionList.GetQuestionList;

public record GetQuestionListInput
{
    public string Text { get; init; }

    public IEnumerable<string> Categories { get; init; }
}
