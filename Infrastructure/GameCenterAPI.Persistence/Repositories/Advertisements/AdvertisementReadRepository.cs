using GameCenterAPI.Application.Repositories.Advertisements;
using GameCenterAPI.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace GameCenterAPI.Persistence.Repositories.Advertisements
{
    public class AdvertisementReadRepository : ReadRepository<Advertisement>, IAdvertisementReadRepository
    {
        public AdvertisementReadRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
