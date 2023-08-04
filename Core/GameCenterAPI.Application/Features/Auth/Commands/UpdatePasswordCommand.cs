using GameCenterAPI.Application.Abstraction.Services;
using MediatR;
using ZstdSharp.Unsafe;

namespace GameCenterAPI.Application.Features.Auth.Commands
{
    public class UpdatePasswordCommandRequest : IRequest<UpdatePasswordCommandResponse>
    {
        public string UserId { get; set; }
        public string ResetToken { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }

    public class UpdatePasswordCommandResponse
    {

    }

    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommandRequest, UpdatePasswordCommandResponse>
    {
        readonly IUserService _userService;

        public UpdatePasswordCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UpdatePasswordCommandResponse> Handle(UpdatePasswordCommandRequest request, CancellationToken cancellationToken)
        {
            if (!request.Password.Equals(request.PasswordConfirm))
                throw new Exception("Şifreler Eşleşmemektedir.");

            await _userService.UpdatePasswordAsync(request.UserId, request.ResetToken, request.Password);

            return new();
        }
    }
}
