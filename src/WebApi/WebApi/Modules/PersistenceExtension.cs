using Application.Repositories;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Modules;

public static class PersistenceExtension
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MyDbContext>((provider, builder) =>
        {
            builder.UseSqlServer(configuration.GetConnectionString("MainDb"));
        }, ServiceLifetime.Transient, ServiceLifetime.Transient);

        services.AddScoped<IUserRepository, UserRepository>();
    }
}
