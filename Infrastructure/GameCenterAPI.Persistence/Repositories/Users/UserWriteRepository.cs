using GameCenterAPI.Application.Repositories.Users;
using GameCenterAPI.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace GameCenterAPI.Persistence.Repositories.Users
{
    public class UserWriteRepository : WriteRepository<User>, IUserWriteRepository
    {
        public UserWriteRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
