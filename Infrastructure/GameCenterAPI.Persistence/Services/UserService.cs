using GameCenterAPI.Application.Abstraction.Services;
using GameCenterAPI.Application.DTOs.User;
using GameCenterAPI.Application.Features.Auth;
using GameCenterAPI.Application.Helpers;
using GameCenterAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace GameCenterAPI.Infrastructure.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public int TotalUsersCount => _userManager.Users.Count();

        public Task<bool> HasRolePermissionToEndpointAsync(string name, string code)
        {
            throw new NotImplementedException();
        }

        public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
        {
            AppUser? user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                resetToken = resetToken.UrlDecode();
                IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
                if (result.Succeeded)
                    await _userManager.UpdateSecurityStampAsync(user);
                else
                    throw new Exception("Bilinmeyen bir hatayla karşılaşıldı.");
            }
        }

        public async Task UpdateRefreshTokenAsync(AppUser user, string refreshToken, DateTime refreshTokenExpirationDate)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiration = refreshTokenExpirationDate;
            IdentityResult result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                throw new Exception(ErrorMessages.UnknownErrorWhenRefreshTokenUpdate);

        }
    }
}
