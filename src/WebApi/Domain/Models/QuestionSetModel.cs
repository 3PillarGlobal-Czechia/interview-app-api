﻿using System.Collections.Generic;

namespace Domain.Models;

public class QuestionSetModel : ModelBase
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public ICollection<QuestionModel> InterviewQuestions { get; set; }
}