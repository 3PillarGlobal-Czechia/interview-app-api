using System.Collections.Generic;

namespace Domain.Models;

public class QuestionListModel : ModelBase
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public ICollection<InterviewQuestionModel> InterviewQuestions { get; set; }
}
