using Domain.Models.ValueObject;
using System.Collections.Generic;

namespace Domain.Models.Views;

public class QuestionSetListItem
{
    public QuestionSetModel QuestionSet { get; init; }
    public Difficulty Difficulty { get; set; }
    public IEnumerable<Category> Tags { get; set; }
}
