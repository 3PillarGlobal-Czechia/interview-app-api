using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace End2EndTests;

public class MyWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.Remove(services.FirstOrDefault(x => x.ServiceType == typeof(DbContextOptions<MyDbContext>)));

            Guid dbId = Guid.NewGuid();
            services.AddDbContext<MyDbContext>(options =>
            {
                options.UseInMemoryDatabase($"InMemoryDbForTests-{dbId}");
            }, ServiceLifetime.Transient, ServiceLifetime.Transient);

            using var scope = services.BuildServiceProvider().CreateScope();
            scope.ServiceProvider.GetRequiredService<MyDbContext>().Database.EnsureCreated();
        });
    }
}
