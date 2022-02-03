using Infrastructure.Entities;
using System;
using System.Collections.Generic;

namespace Infrastructure.Seeds;

internal static class InterviewQuestionSeeder
{
    internal static IEnumerable<InterviewQuestion> GetSeeds()
    {
        DateTime now = DateTime.Now;
        return new List<InterviewQuestion>()
        {
            new InterviewQuestion {
                Title = "C# structures",
                Difficulty = 1,
                Category = "C# concept basics",
                Content = "Describe structures on code example",
                CreatedAt = now,
                UpdatedAt = now
            },
            new InterviewQuestion {
                Title = "C# classes/interfaces",
                Difficulty = 1,
                Category = "C# concept basics",
                Content = "What are classes and interfaces?",
                CreatedAt = now,
                UpdatedAt = now
            },
            new InterviewQuestion {
                Title = "C# polymorphism",
                Difficulty = 2,
                Category = "C# concept basics",
                Content = "What is Polymorphism?",
                CreatedAt = now,
                UpdatedAt = now
            },
            new InterviewQuestion {
                Title = ".NET framework vs core vs .NET",
                Difficulty = 2,
                Category = ".NET specific",
                Content = "What is the difference between .NET Framework, .NET Core and .NET?",
                CreatedAt = now,
                UpdatedAt = now
            },
            new InterviewQuestion {
                Title = ".NET sealed",
                Difficulty = 3,
                Category = ".NET specific",
                Content = "What does the keyword sealed before class mean?",
                CreatedAt = now,
                UpdatedAt = now
            },
            new InterviewQuestion {
                Title = "Relational database",
                Difficulty = 2,
                Category = "SQL",
                Content = "Define what is a relational database?",
                CreatedAt = now,
                UpdatedAt = now
            },
            new InterviewQuestion {
                Title = "ORM",
                Difficulty = 3,
                Category = "ORMS",
                Content = "Define what is an ORM?",
                CreatedAt = now,
                UpdatedAt = now
            },
            new InterviewQuestion {
                Title = "NOSQL",
                Difficulty = 4,
                Category = "NOSQL",
                Content = "What is index, document, shard in ElasticSearch?",
                CreatedAt = now,
                UpdatedAt = now
            }
        };
    }
}
