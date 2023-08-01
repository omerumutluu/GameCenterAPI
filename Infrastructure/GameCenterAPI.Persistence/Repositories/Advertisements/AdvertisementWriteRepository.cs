using GameCenterAPI.Application.Repositories.Advertisements;
using GameCenterAPI.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace GameCenterAPI.Persistence.Repositories.Advertisements
{
    public class AdvertisementWriteRepository : WriteRepository<Advertisement>, IAdvertisementWriteRepository
    {
        public AdvertisementWriteRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
