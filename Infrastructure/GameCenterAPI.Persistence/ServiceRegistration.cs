using GameCenterAPI.Application.Repositories.Advertisements;
using GameCenterAPI.Application.Repositories.Games;
using GameCenterAPI.Application.Repositories.Users;
using GameCenterAPI.Persistence.Repositories.Advertisements;
using GameCenterAPI.Persistence.Repositories.Games;
using GameCenterAPI.Persistence.Repositories.Users;
using Microsoft.Extensions.DependencyInjection;

namespace GameCenterAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddScoped<IUserReadRepository, UserReadRepository>();
            services.AddScoped<IUserWriteRepository,UserWriteRepository>();

            services.AddScoped<IGameReadRepository,GameReadRepository>();
            services.AddScoped<IGameWriteRepository,GameWriteRepository>();

            services.AddScoped<IAdvertisementReadRepository, AdvertisementReadRepository>();
            services.AddScoped<IAdvertisementWriteRepository, AdvertisementWriteRepository>();
        }
    }
}
