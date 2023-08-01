using GameCenterAPI.Application.Abstraction.Token;
using GameCenterAPI.Application.DTOs;
using GameCenterAPI.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GameCenterAPI.Application.Features.Auth.Commands
{
    public class LoginCommandRequest : IRequest<LoginCommandResponse>
    {
        public string EmailOrUsername { get; set; }
        public string Password { get; set; }
    }

    public class LoginCommandResponse
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
        public string UserId { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, LoginCommandResponse>
    {
        readonly SignInManager<AppUser> _signInManager;
        readonly UserManager<AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;

        public LoginCommandHandler(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ITokenHandler tokenHandler)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            AppUser? user = await _userManager.FindByEmailAsync(request.EmailOrUsername);

            if (user == null)
                user = await _userManager.FindByNameAsync(request.EmailOrUsername);

            if (user == null)
                throw new Exception(ErrorMessages.EmailOrUsernameFalse);

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);

            if (!result.Succeeded)
                throw new Exception(ErrorMessages.PasswordIsFalse);

            Token token = _tokenHandler.CreateToken(user);

            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpiration = token.Expiration.AddMinutes(1);

            IdentityResult updateResult = await _userManager.UpdateAsync(user);

            if (updateResult.Succeeded)
                return new()
                {
                    AccessToken =
                    token.AccessToken,
                    Expiration = token.Expiration,
                    RefreshToken = token.RefreshToken,
                    UserId = user.Id
                };

            throw new Exception(ErrorMessages.UnknownErrorWhenUserLogin);
        }
    }
}
