using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace WebApi.Modules
{
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
                    foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
                    {
                        if (env.IsDevelopment() || env.IsEnvironment("local"))
                        {
                            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                                description.GroupName.ToUpperInvariant());
                        }
                        else
                        {
                            options.SwaggerEndpoint($"/accounts-api/swagger/{description.GroupName}/swagger.json", "Accounts-API Reverse proxy");
                        }
                    }
                });

            return app;
        }
    }
}
