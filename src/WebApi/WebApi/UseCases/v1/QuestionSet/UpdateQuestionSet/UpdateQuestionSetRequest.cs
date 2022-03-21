using System.Collections.Generic;

namespace WebApi.UseCases.v1.QuestionSet.UpdateQuestionSet;

public record UpdateQuestionSetRequest
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public IEnumerable<int> QuestionsToAdd { get; set; }

    public IEnumerable<int> QuestionsToRemove { get; set; }
}
