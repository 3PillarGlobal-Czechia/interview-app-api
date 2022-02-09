using System.Collections.Generic;

namespace Application.UseCases.QuestionList.GetQuestionList;

public record GetQuestionListInput
{
    public string Text { get; set; }

    public IEnumerable<string> Categories { get; set; }
}
