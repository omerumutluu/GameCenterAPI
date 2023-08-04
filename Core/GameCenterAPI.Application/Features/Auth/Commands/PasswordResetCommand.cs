using GameCenterAPI.Application.Abstraction.Services;
using MediatR;

namespace GameCenterAPI.Application.Features.Auth.Commands
{
    public class PasswordResetCommandRequest : IRequest<PasswordResetCommandResponse>
    {
        public string Email { get; set; }
    }

    public class PasswordResetCommandResponse
    {

    }

    public class PasswordResetCommandHandler : IRequestHandler<PasswordResetCommandRequest, PasswordResetCommandResponse>
    {
        readonly IAuthService _authService;

        public PasswordResetCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<PasswordResetCommandResponse> Handle(PasswordResetCommandRequest request, CancellationToken cancellationToken)
        {
            await _authService.PasswordResetAsync(request.Email);
            return new();
        }
    }
}
