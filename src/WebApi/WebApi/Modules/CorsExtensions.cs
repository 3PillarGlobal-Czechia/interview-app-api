using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace WebApi.Modules;

public static class CorsExtensions
{
    public static IApplicationBuilder UseCors(this IApplicationBuilder app, IConfiguration configuration)
    {
        var allowedOrigins = configuration["AllowedOrigins"];

        if (allowedOrigins == null)
        {
            return app;
        }

        return app.UseCors(builder =>
        {
            builder.WithOrigins(allowedOrigins.Split(';'))
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
    }
}
