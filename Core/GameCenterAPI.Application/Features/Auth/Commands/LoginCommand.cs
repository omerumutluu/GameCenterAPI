using GameCenterAPI.Application.Abstraction.Services;
using GameCenterAPI.Application.Abstraction.Token;
using GameCenterAPI.Application.DTOs;
using GameCenterAPI.Application.DTOs.User;
using GameCenterAPI.Domain.Entities.Identity;
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
        public Token Token { get; set; }
        public LoginUserResponse User { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, LoginCommandResponse>
    {
        readonly IAuthService _authService;

        public LoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            (LoginUserResponse user, Token token) result = await _authService.LoginAsync(request.EmailOrUsername, request.Password);

            return new() { Token = result.token, User = result.user };
        }
    }
}
