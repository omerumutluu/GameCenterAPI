using GameCenterAPI.Application.Repositories.Users;
using GameCenterAPI.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace GameCenterAPI.Persistence.Repositories.Users
{
    public class UserReadRepository : ReadRepository<User>, IUserReadRepository
    {
        public UserReadRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
