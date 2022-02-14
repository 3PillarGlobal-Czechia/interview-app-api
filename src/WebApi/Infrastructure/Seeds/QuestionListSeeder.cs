using Infrastructure.Entities;
using System;
using System.Collections.Generic;

namespace Infrastructure.Seeds;

internal static class QuestionListSeeder
{
    internal static IEnumerable<QuestionList> GetSeeds()
    {
        var questionList = new List<QuestionList>()
        {
            new QuestionList()
            {
                Title = "C# list",
                Description = "Example C# list with some questions"
            },
            new QuestionList()
            {
                Title = ".NET list",
                Description = "Example .NET list with some questions"
            },
            new QuestionList()
            {
                Title = "SQL list",
                Description = "Example SQL list with some questions"
            }
        };

        DateTime now = DateTime.Now;
        questionList.ForEach(ql =>
        {
            ql.CreatedAt = now;
            ql.UpdatedAt = now;
        });

        return questionList;
    }
}
