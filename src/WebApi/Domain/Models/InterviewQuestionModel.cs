﻿namespace Domain.Models;

public class InterviewQuestionModel : ModelBase
{
    public int Id { get; set; }

    public string Title { get; set; }

    public int? Difficulty { get; set; }

    public string Category { get; set; }

    public string Content { get; set; }
}
