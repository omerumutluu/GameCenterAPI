using GameCenterAPI.Application.Abstraction.Services;
using GameCenterAPI.Application.DTOs;
using MediatR;

namespace GameCenterAPI.Application.Features.Auth.Commands
{
    public class RefreshTokenLoginCommandRequest : IRequest<RefreshTokenLoginCommandResponse>
    {
        public string refreshToken { get; set; }
    }

    public class RefreshTokenLoginCommandResponse
    {
        public Token Token { get; set; }
    }

    public class RefreshTokenLoginCommandHandler : IRequestHandler<RefreshTokenLoginCommandRequest, RefreshTokenLoginCommandResponse>
    {
        readonly IAuthService _authService;

        public RefreshTokenLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommandRequest request, CancellationToken cancellationToken)
        {
            Token token = await _authService.RefreshTokenLoginAsync(request.refreshToken);

            return new() { Token = token };
        }
    }
}
