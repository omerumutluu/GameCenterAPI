using GameCenterAPI.Application.Repositories.Games;
using GameCenterAPI.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace GameCenterAPI.Persistence.Repositories.Games
{
    public class GameReadRepository : ReadRepository<Game>, IGameReadRepository
    {
        public GameReadRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
