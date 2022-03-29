using System.Collections.Generic;

namespace Domain.Models.Agreggates;

public class QuestionSetDetail
{
    public QuestionSetModel QuestionSet { get; init; }

    public IEnumerable<QuestionModel> Questions { get; init; }
}
