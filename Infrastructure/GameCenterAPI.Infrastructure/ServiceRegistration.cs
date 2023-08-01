using GameCenterAPI.Application.Abstraction.Storage;
using GameCenterAPI.Application.Abstraction.Token;
using GameCenterAPI.Infrastructure.Services.Storage;
using GameCenterAPI.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace GameCenterAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<ITokenHandler,TokenHandler>();

            services.AddSingleton<IAzureStorage, AzureStorage>();
        }
    }
}
