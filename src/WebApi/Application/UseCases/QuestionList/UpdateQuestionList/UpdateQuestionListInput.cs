using System.Collections.Generic;

namespace Application.UseCases.QuestionList.UpdateQuestionList;

public readonly struct UpdateQuestionListInput
{
    public int Id { get; init; }

    public string Title { get; init; }

    public string Description { get; init; }

    public IEnumerable<int> QuestionsToAdd { get; init; }

    public IEnumerable<int> QuestionsToRemove { get; init; }
}
