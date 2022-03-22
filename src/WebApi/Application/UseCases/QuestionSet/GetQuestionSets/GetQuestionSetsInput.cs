using System.Collections.Generic;

namespace Application.UseCases.QuestionSet.GetQuestionSets;

public readonly struct GetQuestionSetsInput
{
    public string QueryString { get; init; }

    public IEnumerable<string> Category { get; init; }
}
