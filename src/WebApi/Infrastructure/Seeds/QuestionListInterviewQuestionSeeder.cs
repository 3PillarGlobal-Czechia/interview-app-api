using Infrastructure.Entities;
using System;
using System.Collections.Generic;

namespace Infrastructure.Seeds;

internal static class QuestionListInterviewQuestionSeeder
{
    internal static IEnumerable<QuestionListInterviewQuestion> GetSeeds()
    {
        var questionListInterviewQuestions = new List<QuestionListInterviewQuestion>()
        {
            new QuestionListInterviewQuestion()
            {
                QuestionListId = 1,
                InterviewQuestionId = 1,
                Order = 1
            },
            new QuestionListInterviewQuestion()
            {
                QuestionListId = 1,
                InterviewQuestionId = 2,
                Order = 2
            },
            new QuestionListInterviewQuestion()
            {
                QuestionListId = 1,
                InterviewQuestionId = 3,
                Order = 3
            },
            new QuestionListInterviewQuestion()
            {
                QuestionListId = 2,
                InterviewQuestionId = 4,
                Order = 1
            },
            new QuestionListInterviewQuestion()
            {
                QuestionListId = 2,
                InterviewQuestionId = 5,
                Order = 2
            },
            new QuestionListInterviewQuestion()
            {
                QuestionListId = 3,
                InterviewQuestionId = 6,
                Order = 1
            },
            new QuestionListInterviewQuestion()
            {
                QuestionListId = 3,
                InterviewQuestionId = 8,
                Order = 2
            },
        };

        DateTime now = DateTime.Now;
        questionListInterviewQuestions.ForEach(ql =>
        {
            ql.CreatedAt = now;
            ql.UpdatedAt = now;
        });

        return questionListInterviewQuestions;
    }
}
