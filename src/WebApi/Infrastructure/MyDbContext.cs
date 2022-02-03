using Infrastructure.Entities;
using Infrastructure.Seeds;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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

        modelBuilder.Entity<InterviewQuestion>(builder =>
        {
            builder.HasKey(iq => iq.Id);
        });
    }

    public async Task Seed()
    {
        int questionCount = await InterviewQuestions.CountAsync();
        if (questionCount == 0)
        {
            await InterviewQuestions.AddRangeAsync(InterviewQuestionSeeder.GetSeeds());
            await SaveChangesAsync();
        }
    }
}
