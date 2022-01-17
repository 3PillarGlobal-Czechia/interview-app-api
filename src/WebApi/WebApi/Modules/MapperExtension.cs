using AutoMapper;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Modules;

public static class MapperExtension
{
    public static void AddMapper(this IServiceCollection services)
    {
        // Auto Mapper Configurations
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });

        IMapper mapper = mappingConfig.CreateMapper();

        services.AddSingleton(mapper);
    }
}
