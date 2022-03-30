using Domain.Entities;
using System.Collections.Generic;

namespace Infrastructure.Entities;

public class QuestionList : EntityBase, IEntity
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public ICollection<QuestionListInterviewQuestion> QuestionListInterviewQuestions { get; set; }
}
