using System.Collections.Generic;

namespace Application.UseCases.QuestionList.GetQuestionList;

public readonly struct GetQuestionListInput
{
    public int? Id { get; init; }

    public string Text { get; init; }

    public IEnumerable<string> Categories { get; init; }
}
