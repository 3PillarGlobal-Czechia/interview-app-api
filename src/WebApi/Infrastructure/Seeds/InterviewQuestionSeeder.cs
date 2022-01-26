using Infrastructure.Entities;
using System.Collections.Generic;

namespace Infrastructure.Seeds;

internal static class InterviewQuestionSeeder
{
    internal static IEnumerable<InterviewQuestion> GetSeeds()
    {
        return new List<InterviewQuestion>()
        {
			new InterviewQuestion {
				Id = 1,
				Difficulty = 1,
				Category = "C# concept basics",
				Content = "Describe structures on code example"
			},
			new InterviewQuestion {
				Id = 2,
				Difficulty = 1,
				Category = "C# concept basics",
				Content = "What are classes and interfaces?"
			},
			new InterviewQuestion {
				Id = 3,
				Difficulty = 1,
				Category = "C# concept basics",
				Content = "What access modifiers do you know? (private vs public vs protected)"
			},
			new InterviewQuestion {
				Id = 4,
				Difficulty = 2,
				Category = "C# concept basics",
				Content = "What is Encapsulation?"
			},
			new InterviewQuestion {
				Id = 5,
				Difficulty = 2,
				Category = "C# concept basics",
				Content = "What is Inheritance?"
			},
			new InterviewQuestion {
				Id = 6,
				Difficulty = 2,
				Category = "C# concept basics",
				Content = "What is Polymorphism?"
			},
			new InterviewQuestion {
				Id = 7,
				Difficulty = 2,
				Category = "C# concept basics",
				Content = "What is the difference between Abstract class and interface?"
			},
			new InterviewQuestion {
				Id = 8,
				Difficulty = 2,
				Category = "C# concept basics",
				Content = "Do you know these acronyms SOLID / CUPID?"
			},
			new InterviewQuestion {
				Id = 9,
				Difficulty = 2,
				Category = "C# concept basics",
				Content = "What is overriding and overloading?"
			},
			new InterviewQuestion {
				Id = 10,
				Difficulty = 3,
				Category = "C# concept basics",
				Content = "What is meant by “composition over inheritance” ?"
			},
			new InterviewQuestion {
				Id = 11,
				Difficulty = 2,
				Category = "C# concept basics",
				Content = "What is a generic class/data type?"
			},
			new InterviewQuestion {
				Id = 12,
				Difficulty = 3,
				Category = "C# concept basics",
				Content = "What's the difference between reference data type and value data type?"
			},
			new InterviewQuestion {
				Id = 13,
				Difficulty = 4,
				Category = "C# concept basics",
				Content = "What design patterns do you know? "
			},
			new InterviewQuestion {
				Id = 14,
				Difficulty = 4,
				Category = "C# concept basics",
				Content = "Factory, Singleton, Builder, Strategy?"
			},
			new InterviewQuestion {
				Id = 15,
				Difficulty = 5,
				Category = "C# concept basics",
				Content = "What other programming paradigms do you know?"
			},
			new InterviewQuestion {
				Id = 16,
				Difficulty = 1,
				Category = ".NET specific",
				Content = "What is your experience with testing? "
			},
			new InterviewQuestion {
				Id = 17,
				Difficulty = 1,
				Category = ".NET specific",
				Content = "What is the difference between unit, integration and e2e testing?"
			},
			new InterviewQuestion {
				Id = 18,
				Difficulty = 1,
				Category = ".NET specific",
				Content = "What other testing approaches do you know?"
			},
			new InterviewQuestion {
				Id = 19,
				Difficulty = 2,
				Category = ".NET specific",
				Content = "What does AAA mean in unit tests?"
			},
			new InterviewQuestion {
				Id = 20,
				Difficulty = 2,
				Category = ".NET specific",
				Content = "What is mock and fixture?"
			},
			new InterviewQuestion {
				Id = 21,
				Difficulty = 2,
				Category = ".NET specific",
				Content = "What is relationship between .NET and C#?"
			},
			new InterviewQuestion {
				Id = 22,
				Difficulty = 2,
				Category = ".NET specific",
				Content = "What frameworks did you work with?"
			},
			new InterviewQuestion {
				Id = 23,
				Difficulty = 2,
				Category = ".NET specific",
				Content = "What is the difference between .NET Framework, .NET Core and .NET?"
			},
			new InterviewQuestion {
				Id = 24,
				Difficulty = 3,
				Category = ".NET specific",
				Content = "What does the keyword sealed before class mean?"
			},
			new InterviewQuestion {
				Id = 25,
				Difficulty = 3,
				Category = ".NET specific",
				Content = "What about other keywords? Virtual, Abstract, Override"
			},
			new InterviewQuestion {
				Id = 26,
				Difficulty = 2,
				Category = ".NET specific",
				Content = "What is the difference between Class, Struct and Record?"
			},
			new InterviewQuestion {
				Id = 27,
				Difficulty = 2,
				Category = ".NET specific",
				Content = "What is a constant?"
			},
			new InterviewQuestion {
				Id = 28,
				Difficulty = 3,
				Category = ".NET specific",
				Content = "What is the difference between constant and readonly property?"
			},
			new InterviewQuestion {
				Id = 29,
				Difficulty = 3,
				Category = ".NET specific",
				Content = "What is difference between readonly property and property with private set"
			},
			new InterviewQuestion {
				Id = 30,
				Difficulty = 3,
				Category = ".NET specific",
				Content = "What is asynchronous programming?"
			},
			new InterviewQuestion {
				Id = 31,
				Difficulty = 2,
				Category = ".NET specific",
				Content = "How can this be implemented in .NET? "
			},
			new InterviewQuestion {
				Id = 32,
				Difficulty = 3,
				Category = ".NET specific",
				Content = "What is Multithreading, processes, threads, locks ?"
			},
			new InterviewQuestion {
				Id = 33,
				Difficulty = 3,
				Category = ".NET specific",
				Content = "What is a garbage collector?"
			},
			new InterviewQuestion {
				Id = 34,
				Difficulty = 3,
				Category = ".NET specific",
				Content = "What are the differences between ref and out keywords ?"
			},
			new InterviewQuestion {
				Id = 35,
				Difficulty = 5,
				Category = ".NET specific",
				Content = "Describe .NET build process"
			},
			new InterviewQuestion {
				Id = 36,
				Difficulty = 4,
				Category = ".NET specific",
				Content = "Describe items in the .NET ecosystem? (Solution, assembly, project)"
			},
			new InterviewQuestion {
				Id = 37,
				Difficulty = 4,
				Category = ".NET specific",
				Content = "What does access modifier internal mean?"
			},
			new InterviewQuestion {
				Id = 38,
				Difficulty = 5,
				Category = ".NET specific",
				Content = "Describe acronyms JIT, CLS, CIL, CLR, AOT compilation"
			},
			new InterviewQuestion {
				Id = 39,
				Difficulty = 4,
				Category = ".NET specific",
				Content = "What is a delegate and event? "
			},
			new InterviewQuestion {
				Id = 40,
				Difficulty = 4,
				Category = ".NET specific",
				Content = "What is data type Action and Func?"
			},
			new InterviewQuestion {
				Id = 41,
				Difficulty = 2,
				Category = "SQL",
				Content = "Define what is a relational database?"
			},
			new InterviewQuestion {
				Id = 42,
				Difficulty = 2,
				Category = "SQL",
				Content = "What is a stored procedure?"
			},
			new InterviewQuestion {
				Id = 43,
				Difficulty = 3,
				Category = "SQL",
				Content = "What is an index?"
			},
			new InterviewQuestion {
				Id = 44,
				Difficulty = 3,
				Category = "SQL",
				Content = "What is a code first and schema first approach?"
			},
			new InterviewQuestion {
				Id = 45,
				Difficulty = 3,
				Category = "SQL",
				Content = "What are common problems in relational databases?"
			},
			new InterviewQuestion {
				Id = 46,
				Difficulty = 3,
				Category = "ORMS",
				Content = "Define what is an ORM?"
			},
			new InterviewQuestion {
				Id = 47,
				Difficulty = 3,
				Category = "ORMS",
				Content = "Do you know any diagnostic tools? (transactions,  N+1, Indexing, searching)"
			},
			new InterviewQuestion {
				Id = 48,
				Difficulty = 2,
				Category = "NOSQL",
				Content = "What is NoSQL?"
			},
			new InterviewQuestion {
				Id = 49,
				Difficulty = 4,
				Category = "NOSQL",
				Content = "What is index, document, shard in ElasticSearch?"
			},
			new InterviewQuestion {
				Id = 50,
				Difficulty = 2,
				Category = "EF",
				Content = "What is a Migration?"
			},
			new InterviewQuestion {
				Id = 51,
				Difficulty = 2,
				Category = "EF",
				Content = "What's LINQ?"
			},
			new InterviewQuestion {
				Id = 52,
				Difficulty = 2,
				Category = "EF",
				Content = "What is an Entity?"
			},
			new InterviewQuestion {
				Id = 53,
				Difficulty = 2,
				Category = "EF",
				Content = "What is a Context?"
			},
			new InterviewQuestion {
				Id = 54,
				Difficulty = 3,
				Category = "Cloud/infrastructure as a service",
				Content = "Experience with specific cloud providers?"
			},
			new InterviewQuestion {
				Id = 55,
				Difficulty = 3,
				Category = "Cloud/infrastructure as a service",
				Content = "CI/CD Pipelines?"
			},
			new InterviewQuestion {
				Id = 56,
				Difficulty = 3,
				Category = "Cloud/infrastructure as a service",
				Content = "Docker?"
			},
			new InterviewQuestion {
				Id = 57,
				Difficulty = 3,
				Category = "Cloud/infrastructure as a service",
				Content = "Kubernetes?"
			},
			new InterviewQuestion {
				Id = 58,
				Difficulty = 3,
				Category = "Cloud/infrastructure as a service",
				Content = "Serverless?"
			},
			new InterviewQuestion {
				Id = 59,
				Difficulty = 3,
				Category = "Microservices",
				Content = "Define microservices?"
			},
			new InterviewQuestion {
				Id = 60,
				Difficulty = 3,
				Category = "Microservices",
				Content = "What is the difference between service oriented architecture?"
			},
			new InterviewQuestion {
				Id = 61,
				Difficulty = 3,
				Category = "Microservices",
				Content = "Experience with development, deployment?"
			},
			new InterviewQuestion {
				Id = 62,
				Difficulty = 4,
				Category = "Microservices",
				Content = "How would you set up communication between the individual services?"
			},
			new InterviewQuestion {
				Id = 63,
				Difficulty = 4,
				Category = "Microservices",
				Content = "PUB/SUB pattern?"
			},
			new InterviewQuestion {
				Id = 64,
				Difficulty = 2,
				Category = "Version control",
				Content = "Experience with different versioning software?"
			},
			new InterviewQuestion {
				Id = 65,
				Difficulty = 3,
				Category = "Version control",
				Content = "Merge vs Rebase vs Cherry Pick?"
			},
			new InterviewQuestion {
				Id = 66,
				Difficulty = 3,
				Category = "Version control",
				Content = "What kind of workflows do you generally use?"
			},
			new InterviewQuestion {
				Id = 67,
				Difficulty = 3,
				Category = "Version control",
				Content = "What is the biggest mistake one can do in Git?"
			},
			new InterviewQuestion {
				Id = 68,
				Difficulty = 4,
				Category = "Design patterns",
				Content = "Experience?"
			},
			new InterviewQuestion {
				Id = 69,
				Difficulty = 4,
				Category = "Design patterns",
				Content = "Any architecture / design pattern you would like to try out?"
			}
		};
    }
}
