namespace Domain.Models;

public class QuestionSetQuestionModel : ModelBase
{
    public int Order { get; set; }

    public int QuestionListId { get; set; }

    public int InterviewQuestionId { get; set; }

    public QuestionSetModel QuestionList { get; set; }

    public QuestionModel InterviewQuestion { get; set; }
}
