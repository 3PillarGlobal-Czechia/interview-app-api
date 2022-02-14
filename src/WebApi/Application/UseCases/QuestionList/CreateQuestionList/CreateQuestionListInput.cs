﻿using System.Collections.Generic;

namespace Application.UseCases.QuestionList.CreateQuestionList;

public record CreateQuestionListInput
{
    public string Title { get; init; }

    public string Description { get; init; }

    public IEnumerable<int> InterviewQuestionIds { get; init; }
}
