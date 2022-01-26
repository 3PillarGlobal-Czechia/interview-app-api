using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InterviewQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Difficulty = table.Column<int>(type: "int", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewQuestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "InterviewQuestions",
                columns: new[] { "Id", "Category", "Content", "Difficulty", "Title" },
                values: new object[,]
                {
                    { 1, "C# concept basics", "Describe structures on code example", 1, null },
                    { 2, "C# concept basics", "What are classes and interfaces?", 1, null },
                    { 3, "C# concept basics", "What access modifiers do you know? (private vs public vs protected)", 1, null },
                    { 4, "C# concept basics", "What is Encapsulation?", 2, null },
                    { 5, "C# concept basics", "What is Inheritance?", 2, null },
                    { 6, "C# concept basics", "What is Polymorphism?", 2, null },
                    { 7, "C# concept basics", "What is the difference between Abstract class and interface?", 2, null },
                    { 8, "C# concept basics", "Do you know these acronyms SOLID / CUPID?", 2, null },
                    { 9, "C# concept basics", "What is overriding and overloading?", 2, null },
                    { 10, "C# concept basics", "What is meant by “composition over inheritance” ?", 3, null },
                    { 11, "C# concept basics", "What is a generic class/data type?", 2, null },
                    { 12, "C# concept basics", "What's the difference between reference data type and value data type?", 3, null },
                    { 13, "C# concept basics", "What design patterns do you know? ", 4, null },
                    { 14, "C# concept basics", "Factory, Singleton, Builder, Strategy?", 4, null },
                    { 15, "C# concept basics", "What other programming paradigms do you know?", 5, null },
                    { 16, ".NET specific", "What is your experience with testing? ", 1, null },
                    { 17, ".NET specific", "What is the difference between unit, integration and e2e testing?", 1, null },
                    { 18, ".NET specific", "What other testing approaches do you know?", 1, null },
                    { 19, ".NET specific", "What does AAA mean in unit tests?", 2, null },
                    { 20, ".NET specific", "What is mock and fixture?", 2, null },
                    { 21, ".NET specific", "What is relationship between .NET and C#?", 2, null },
                    { 22, ".NET specific", "What frameworks did you work with?", 2, null },
                    { 23, ".NET specific", "What is the difference between .NET Framework, .NET Core and .NET?", 2, null },
                    { 24, ".NET specific", "What does the keyword sealed before class mean?", 3, null },
                    { 25, ".NET specific", "What about other keywords? Virtual, Abstract, Override", 3, null },
                    { 26, ".NET specific", "What is the difference between Class, Struct and Record?", 2, null },
                    { 27, ".NET specific", "What is a constant?", 2, null },
                    { 28, ".NET specific", "What is the difference between constant and readonly property?", 3, null },
                    { 29, ".NET specific", "What is difference between readonly property and property with private set", 3, null },
                    { 30, ".NET specific", "What is asynchronous programming?", 3, null },
                    { 31, ".NET specific", "How can this be implemented in .NET? ", 2, null },
                    { 32, ".NET specific", "What is Multithreading, processes, threads, locks ?", 3, null },
                    { 33, ".NET specific", "What is a garbage collector?", 3, null },
                    { 34, ".NET specific", "What are the differences between ref and out keywords ?", 3, null },
                    { 35, ".NET specific", "Describe .NET build process", 5, null },
                    { 36, ".NET specific", "Describe items in the .NET ecosystem? (Solution, assembly, project)", 4, null },
                    { 37, ".NET specific", "What does access modifier internal mean?", 4, null },
                    { 38, ".NET specific", "Describe acronyms JIT, CLS, CIL, CLR, AOT compilation", 5, null },
                    { 39, ".NET specific", "What is a delegate and event? ", 4, null },
                    { 40, ".NET specific", "What is data type Action and Func?", 4, null },
                    { 41, "SQL", "Define what is a relational database?", 2, null },
                    { 42, "SQL", "What is a stored procedure?", 2, null }
                });

            migrationBuilder.InsertData(
                table: "InterviewQuestions",
                columns: new[] { "Id", "Category", "Content", "Difficulty", "Title" },
                values: new object[,]
                {
                    { 43, "SQL", "What is an index?", 3, null },
                    { 44, "SQL", "What is a code first and schema first approach?", 3, null },
                    { 45, "SQL", "What are common problems in relational databases?", 3, null },
                    { 46, "ORMS", "Define what is an ORM?", 3, null },
                    { 47, "ORMS", "Do you know any diagnostic tools? (transactions,  N+1, Indexing, searching)", 3, null },
                    { 48, "NOSQL", "What is NoSQL?", 2, null },
                    { 49, "NOSQL", "What is index, document, shard in ElasticSearch?", 4, null },
                    { 50, "EF", "What is a Migration?", 2, null },
                    { 51, "EF", "What's LINQ?", 2, null },
                    { 52, "EF", "What is an Entity?", 2, null },
                    { 53, "EF", "What is a Context?", 2, null },
                    { 54, "Cloud/infrastructure as a service", "Experience with specific cloud providers?", 3, null },
                    { 55, "Cloud/infrastructure as a service", "CI/CD Pipelines?", 3, null },
                    { 56, "Cloud/infrastructure as a service", "Docker?", 3, null },
                    { 57, "Cloud/infrastructure as a service", "Kubernetes?", 3, null },
                    { 58, "Cloud/infrastructure as a service", "Serverless?", 3, null },
                    { 59, "Microservices", "Define microservices?", 3, null },
                    { 60, "Microservices", "What is the difference between service oriented architecture?", 3, null },
                    { 61, "Microservices", "Experience with development, deployment?", 3, null },
                    { 62, "Microservices", "How would you set up communication between the individual services?", 4, null },
                    { 63, "Microservices", "PUB/SUB pattern?", 4, null },
                    { 64, "Version control", "Experience with different versioning software?", 2, null },
                    { 65, "Version control", "Merge vs Rebase vs Cherry Pick?", 3, null },
                    { 66, "Version control", "What kind of workflows do you generally use?", 3, null },
                    { 67, "Version control", "What is the biggest mistake one can do in Git?", 3, null },
                    { 68, "Design patterns", "Experience?", 4, null },
                    { 69, "Design patterns", "Any architecture / design pattern you would like to try out?", 4, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InterviewQuestions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
