using System.Collections.Generic;

namespace WebApi.UseCases.v1.QuestionList.GetQuestionList;

public record GetQuestionListRequest
{
    public int? Id { get; set; }

    public string Text { get; set; }

    public IEnumerable<string> Categories { get; set; }
}
