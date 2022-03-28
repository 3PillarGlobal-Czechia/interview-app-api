using Infrastructure.Entities;
using Infrastructure.Seeds;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure;

public class MyDbContext : DbContext
{
    public DbSet<InterviewQuestion> InterviewQuestions { get; set; }
    public DbSet<QuestionList> QuestionLists { get; set; }
    public DbSet<QuestionListInterviewQuestion> QuestionListInterviewQuestions { get; set; }

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
        });

        modelBuilder.Entity<QuestionListInterviewQuestion>(builder =>
        {
            builder.HasKey(qliq => new { qliq.QuestionListId, qliq.InterviewQuestionId });
            builder.HasOne(qliq => qliq.QuestionList).WithMany(ql => ql.QuestionListInterviewQuestions);
            builder.HasOne(qliq => qliq.InterviewQuestion).WithMany(iq => iq.QuestionListInterviewQuestions);
        });
    }

    public async Task Seed()
    {
        int questionCount = await InterviewQuestions.CountAsync();
        if (questionCount == 0)
        {
            var interviewQuestions = InterviewQuestionSeeder.GetSeeds();
            await InterviewQuestions.AddRangeAsync(interviewQuestions);
            await SaveChangesAsync();
        }

        int questionListCount = await QuestionLists.CountAsync();
        if (questionListCount == 0)
        {
            var questionLists = QuestionListSeeder.GetSeeds();
            await QuestionLists.AddRangeAsync(questionLists);
            await SaveChangesAsync();
        }
    }
}
