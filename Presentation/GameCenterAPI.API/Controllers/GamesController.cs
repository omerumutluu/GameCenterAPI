using GameCenterAPI.API.Controllers.Base;
using GameCenterAPI.Application.Features.Games.Commands;
using GameCenterAPI.Application.Features.Games.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameCenterAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : BaseController
    {
        public GamesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            Console.WriteLine("user=>", claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var request = new GetAllGamesQueryRequest();
            GetAllGamesQueryResponse response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var request = new GetByIdGameQueryRequest();
            request.Id = id;

            GetByIdGameQueryResponse response = await Mediator.Send(request);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddGameCommandRequest request)
        {
            AddGameCommandResponse response = await Mediator.Send(request);
            return Created("Game", response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateGameCommandRequest request)
        {
            UpdateGameCommandResponse response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            DeleteGameCommandRequest request = new() { Id = id };
            DeleteGameCommandResponse response = await Mediator.Send(request);
            return Ok(response);
        }
    }
}
