using GameCenterAPI.API.Controllers.Base;
using GameCenterAPI.Application.Abstraction.Token;
using GameCenterAPI.Application.Exceptions;
using GameCenterAPI.Application.Features.Auth.Commands;
using GameCenterAPI.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    }
}
