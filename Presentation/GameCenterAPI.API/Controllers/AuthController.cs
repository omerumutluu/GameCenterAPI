using GameCenterAPI.API.Controllers.Base;
using GameCenterAPI.Application.Features.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameCenterAPI.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginCommandRequest request)
        {
            LoginCommandResponse response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterCommandRequest request)
        {
            RegisterCommandResponse response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("refresh-token-login")]
        public async Task<IActionResult> RefreshTokenLogin(RefreshTokenLoginCommandRequest request)
        {
            RefreshTokenLoginCommandResponse response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("password-reset")]
        public async Task<IActionResult> PasswordReset([FromBody] PasswordResetCommandRequest request)
        {
            PasswordResetCommandResponse response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("verify-reset-token")]
        public async Task<IActionResult> VerifyResetToken(VerifyResetTokenCommandRequest request)
        {
            VerifyResetTokenCommandResponse response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommandRequest request)
        {
            UpdatePasswordCommandResponse response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("verify-email-confirm-token")]
        public async Task<IActionResult> VerifyEmailConfirmToken()
        {

            return Ok();
        }

    }
}
