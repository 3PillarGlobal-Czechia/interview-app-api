using System.Collections.Generic;

namespace Application.UseCases.QuestionSet.UpdateQuestionSet;

public record UpdateQuestionSetInput
{
    public int Id { get; init; }

    public string Title { get; init; }

    public string Description { get; init; }

    public IEnumerable<int> QuestionsToAdd { get; init; }

    public IEnumerable<int> QuestionsToRemove { get; init; }
}
