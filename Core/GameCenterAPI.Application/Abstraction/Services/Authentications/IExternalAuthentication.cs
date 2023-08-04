
namespace GameCenterAPI.Application.Abstraction.Services.Authentications
{
    public interface IExternalAuthentication
    {
        Task<DTOs.Token> FacebookLoginAsync(string authToken);
        Task<DTOs.Token> GoogleLoginAsync(string authToken);

    }
}
