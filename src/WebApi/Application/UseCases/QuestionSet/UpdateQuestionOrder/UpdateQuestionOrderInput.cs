using System.Collections.Generic;

namespace Application.UseCases.QuestionSet.UpdateQuestionOrder;

public readonly struct UpdateQuestionOrderInput
{
    public int QuestionSetId { get; init; }

    public IList<int> OrderedQuestionIds { get; init; }
}
