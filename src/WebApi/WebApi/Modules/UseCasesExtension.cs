using Application.UseCases.User.GetUser;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Modules
{
    public static class UseCasesExtension
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            _ = services.AddTransient<IGetUserUseCase, GetUserUseCase>();
            return services;
        }
    }
}
