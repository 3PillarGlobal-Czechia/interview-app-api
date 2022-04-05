using System.Collections.Generic;

namespace WebApi.UseCases.v1.QuestionSet.UpdateQuestionOrder;

public record UpdateQuestionOrderRequest
{
    public IList<int> OrderedQuestionIds { get; set; }
}
