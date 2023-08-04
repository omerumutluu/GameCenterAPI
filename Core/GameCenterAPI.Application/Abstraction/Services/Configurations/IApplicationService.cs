using GameCenterAPI.Application.DTOs.Configuration;

namespace GameCenterAPI.Application.Abstraction.Services.Configurations
{
    public interface IApplicationService
    {
        List<Menu> GetAuthorizeDefinitionEndpoints(Type type);
    }
}
