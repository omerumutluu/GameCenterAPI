using GameCenterAPI.Application.Abstraction.Services;
using MediatR;

namespace GameCenterAPI.Application.Features.Auth.Commands
{
    public class VerifyResetTokenCommandRequest : IRequest<VerifyResetTokenCommandResponse>
    {
        public string ResetToken { get; set; }
        public string UserId { get; set; }
    }

    public class VerifyResetTokenCommandResponse
    {
        public bool IsVerified { get; set; }
    }

    public class VerifyResetTokenCommandHandler : IRequestHandler<VerifyResetTokenCommandRequest, VerifyResetTokenCommandResponse>
    {
        readonly IAuthService _authService;

        public VerifyResetTokenCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<VerifyResetTokenCommandResponse> Handle(VerifyResetTokenCommandRequest request, CancellationToken cancellationToken)
        {
            bool result = await _authService.VerifyResetTokenAsync(request.ResetToken, request.UserId);
            return new() { IsVerified = result };
        }
    }
}
