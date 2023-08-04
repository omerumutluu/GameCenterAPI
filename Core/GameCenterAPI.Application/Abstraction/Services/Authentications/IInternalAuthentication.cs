using GameCenterAPI.Application.DTOs.User;

namespace GameCenterAPI.Application.Abstraction.Services.Authentications
{
    public interface IInternalAuthentication
    {
        Task<(LoginUserResponse user, DTOs.Token token)> LoginAsync(string emailOrUsername, string password);
        Task<bool> RegisterAsync(RegisterUser model);
        Task<DTOs.Token> RefreshTokenLoginAsync(string refreshToken);
    }
}
