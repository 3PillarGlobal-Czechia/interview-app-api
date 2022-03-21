using System.Collections.Generic;

namespace WebApi.UseCases.v1.QuestionSet.GetQuestionSet;

public record GetQuestionSetRequest
{
    public int? Id { get; set; }

    public string Text { get; set; }

    public IEnumerable<string> Categories { get; set; }
}
