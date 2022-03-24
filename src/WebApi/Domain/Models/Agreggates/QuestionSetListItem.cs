using System.Collections.Generic;

namespace Domain.Models.Agreggates;

public class QuestionSetListItem
{
    public QuestionSetModel questionSet { get; init; }

    public IEnumerable<QuestionModel> questions { get; init; }
}
