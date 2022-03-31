using Domain.Entities;

namespace Infrastructure.Entities;

public class QuestionListInterviewQuestion : EntityBase, IEntity
{
    public int Order { get; set; }

    public int QuestionListId { get; set; }

    public int InterviewQuestionId { get; set; }

    public QuestionList QuestionList { get; set; }

    public InterviewQuestion InterviewQuestion { get; set; }
}
