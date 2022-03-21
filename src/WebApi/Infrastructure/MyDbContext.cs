using Infrastructure.Entities;
using Infrastructure.Seeds;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure;

public class MyDbContext : DbContext
{
    public DbSet<InterviewQuestion> InterviewQuestions { get; set; }
    public DbSet<QuestionList> QuestionLists { get; set; }

    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InterviewQuestion>(builder =>
        {
            builder.HasKey(iq => iq.Id);
        });

        modelBuilder.Entity<QuestionList>(builder =>
        {
            builder.HasKey(ql => ql.Id);
            builder.HasMany(ql => ql.InterviewQuestions)
                   .WithMany(iq => iq.QuestionLists);
        });
    }

    public async Task Seed()
    {
        IEnumerable<InterviewQuestion> interviewQuestions = Enumerable.Empty<InterviewQuestion>();
        IEnumerable<QuestionList> questionLists = Enumerable.Empty<QuestionList>();
        int questionCount = await InterviewQuestions.CountAsync();
        if (questionCount == 0)
        {
            interviewQuestions = InterviewQuestionSeeder.GetSeeds();
            await InterviewQuestions.AddRangeAsync(interviewQuestions);
            await SaveChangesAsync();
        }

        int questionListCount = await QuestionLists.CountAsync();
        if (questionListCount == 0)
        {
            questionLists = QuestionListSeeder.GetSeeds();
            await QuestionLists.AddRangeAsync(questionLists);
            await SaveChangesAsync();
        }

        if (questionCount == 0 && questionListCount == 0)
        {
            string[] categories = { "C#", ".NET", "SQL" };
            int index = 0;
            foreach (var questionList in questionLists)
            {
                questionList.InterviewQuestions = new List<InterviewQuestion>();
                var category = categories[index++];
                foreach (var interviewQuestion in interviewQuestions.Where(iq => iq.Category.Contains(category)))
                {
                    questionList.InterviewQuestions.Add(interviewQuestion);
                }
            }

            QuestionLists.UpdateRange(questionLists);
            await SaveChangesAsync();
        }
    }
}
