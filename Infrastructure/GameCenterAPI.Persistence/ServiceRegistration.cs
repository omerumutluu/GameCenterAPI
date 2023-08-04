using GameCenterAPI.Application.Abstraction.Services;
using GameCenterAPI.Application.Repositories.Advertisements;
using GameCenterAPI.Application.Repositories.Games;
using GameCenterAPI.Infrastructure.Services;
using GameCenterAPI.Persistence.Repositories.Advertisements;
using GameCenterAPI.Persistence.Repositories.Games;
using GameCenterAPI.Persistence.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GameCenterAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddScoped<IGameReadRepository,GameReadRepository>();
            services.AddScoped<IGameWriteRepository,GameWriteRepository>();

            services.AddScoped<IAdvertisementReadRepository, AdvertisementReadRepository>();
            services.AddScoped<IAdvertisementWriteRepository, AdvertisementWriteRepository>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
