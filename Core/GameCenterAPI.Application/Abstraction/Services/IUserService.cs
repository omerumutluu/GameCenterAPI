using GameCenterAPI.Application.DTOs.User;
using GameCenterAPI.Domain.Entities.Identity;

namespace GameCenterAPI.Application.Abstraction.Services
{
    public interface IUserService
    {
        Task UpdateRefreshTokenAsync(AppUser user, string refreshToken, DateTime refreshTokenExpirationDate);
        Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);
        //Task<List<>> GetAllUsersAsync(int page, int size);
        int TotalUsersCount { get; }
        Task<bool> HasRolePermissionToEndpointAsync(string name, string code);
    }
}
