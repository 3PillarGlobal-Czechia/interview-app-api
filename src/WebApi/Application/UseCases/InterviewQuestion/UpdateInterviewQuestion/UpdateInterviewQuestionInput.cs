﻿namespace Application.UseCases.InterviewQuestion.UpdateInterviewQuestion;

public record UpdateInterviewQuestionInput
{
    public int Id { get; init; }

    public string Title { get; init; }

    public int? Difficulty { get; init; }

    public string Category { get; init; }

    public string Content { get; init; }
}
