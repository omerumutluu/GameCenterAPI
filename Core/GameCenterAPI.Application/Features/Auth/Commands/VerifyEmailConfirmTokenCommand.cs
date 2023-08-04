using GameCenterAPI.Application.Abstraction.Services;
using MediatR;

namespace GameCenterAPI.Application.Features.Auth.Commands
{
    public class VerifyEmailConfirmTokenCommandRequest : IRequest<VerifyEmailConfirmTokenCommandResponse>
    {
        public string UserId { get; set; }
        public string EmailConfirmToken { get; set; }
    }

    public class VerifyEmailConfirmTokenCommandResponse
    {
        public bool IsVerified { get; set; }
    }

    public class VerifyEmailConfirmTokenCommandHandler : IRequestHandler<VerifyEmailConfirmTokenCommandRequest, VerifyEmailConfirmTokenCommandResponse>
    {
        readonly IAuthService _authService;

        public VerifyEmailConfirmTokenCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<VerifyEmailConfirmTokenCommandResponse> Handle(VerifyEmailConfirmTokenCommandRequest request, CancellationToken cancellationToken)
        {
            bool result = await _authService.VerifyEmailConfirmTokenAsync(request.UserId, request.EmailConfirmToken);

            return new() { IsVerified = result };
        }
    }
}
