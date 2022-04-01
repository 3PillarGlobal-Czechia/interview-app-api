using Infrastructure.Entities;
using System;
using System.Collections.Generic;

namespace Infrastructure.Seeds;

internal static class QuestionListInterviewQuestionSeeder
{
    internal static IEnumerable<QuestionListInterviewQuestion> GetSeeds()
    {
        var relations = new List<QuestionListInterviewQuestion>()
        {
            new QuestionListInterviewQuestion
            {
                QuestionListId = 1,
                InterviewQuestionId = 1,
                Order = 1,
            },
            new QuestionListInterviewQuestion
            {
                QuestionListId = 1,
                InterviewQuestionId = 2,
                Order = 2,
            },
            new QuestionListInterviewQuestion
            {
                QuestionListId = 1,
                InterviewQuestionId = 3,
                Order = 3,
            }
        };

        DateTime now = DateTime.Now;
        relations.ForEach(r =>
        {
            r.CreatedAt = now;
            r.UpdatedAt = now;
        });

        return relations;
    }
}
