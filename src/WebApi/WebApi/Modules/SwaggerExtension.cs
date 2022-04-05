using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace WebApi.Modules;

public static class SwaggerExtension
{
    /// <summary>
    ///     Add Swagger Configuration dependencies.
    /// </summary>
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        _ = services.AddSwaggerGen();
        return services;
    }

    /// <summary>
    ///     Add Swagger dependencies.
    /// </summary>
    public static IApplicationBuilder UseVersionedSwagger(
        this IApplicationBuilder app,
        IApiVersionDescriptionProvider provider,
        IConfiguration configuration,
        IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI(
            options =>
            {
                foreach (string groupName in provider.ApiVersionDescriptions.Select(description => description.GroupName))
                {
                    options.SwaggerEndpoint($"/swagger/{groupName}/swagger.json",
                        groupName.ToUpperInvariant());
                }
            });

        return app;
    }
}
