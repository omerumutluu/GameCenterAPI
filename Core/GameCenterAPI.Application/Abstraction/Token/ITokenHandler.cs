using GameCenterAPI.Domain.Entities.Identity;

namespace GameCenterAPI.Application.Abstraction.Token
{
    public interface ITokenHandler
    {
        DTOs.Token CreateToken(AppUser user);
    }
}
