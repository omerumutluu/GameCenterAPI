using GameCenterAPI.Application.Abstraction.Services.Authentications;

namespace GameCenterAPI.Application.Abstraction.Services
{
    public interface IAuthService : IExternalAuthentication, IInternalAuthentication
    {
        Task PasswordResetAsync(string email);
        Task<bool> VerifyResetTokenAsync(string resetToken, string userId);
        Task<bool> VerifyEmailConfirmTokenAsync(string userId, string emailConfirmToken);
    }
}
