using Infrastructure.Entities;
using Infrastructure.Seeds;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class MyDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<InterviewQuestion> InterviewQuestions { get; set; }

    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(user => user.Id);

        var interviewQuestionBuilder = modelBuilder.Entity<InterviewQuestion>();
        interviewQuestionBuilder.HasKey(interviewQuestion => interviewQuestion.Id);
        interviewQuestionBuilder.HasData(InterviewQuestionSeeder.GetSeeds());
    }
}
