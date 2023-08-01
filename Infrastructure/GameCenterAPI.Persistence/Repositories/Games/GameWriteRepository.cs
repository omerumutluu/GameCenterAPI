using GameCenterAPI.Application.Repositories.Games;
using GameCenterAPI.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace GameCenterAPI.Persistence.Repositories.Games
{
    public class GameWriteRepository : WriteRepository<Game>, IGameWriteRepository
    {
        public GameWriteRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
