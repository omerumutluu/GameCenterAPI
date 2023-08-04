using GameCenterAPI.API.Controllers.Base;
using GameCenterAPI.Application.Abstraction.Services;
using GameCenterAPI.Application.Abstraction.Storage;
using GameCenterAPI.Application.Consts;
using GameCenterAPI.Application.CustomAttributes;
using GameCenterAPI.Application.Enums;
using GameCenterAPI.Application.Features.Games.Commands;
using GameCenterAPI.Application.Features.Games.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameCenterAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : BaseController
    {
        readonly IAzureStorage _azurStorage;
        public GamesController(IMediator mediator, IAzureStorage azurStorage) : base(mediator)
        {
            _azurStorage = azurStorage;
        }

        //public GamesController(IMediator mediator) : base(mediator)
        //{
        //}

        [HttpPost("deneme")]
        public async Task<IActionResult> get()
        {
            IFormFileCollection deneme = Request.Form.Files;
            var result = await _azurStorage.UploadAsync("game-images", deneme);
            Console.WriteLine(result.Select(f => f.containerName).ToString());
            Console.WriteLine(result.Select(f => f.fileName).ToString());
            return Ok();
        }

        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Games, ActionType = ActionType.Reading, Definition = "Get Games")]
        public async Task<IActionResult> Get()
        {
            var request = new GetAllGamesQueryRequest();
            GetAllGamesQueryResponse response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Games, ActionType = ActionType.Reading, Definition = "Get Game By GameId")]
        public async Task<IActionResult> GetById(GetByIdGameQueryRequest request)
        {
            GetByIdGameQueryResponse response = await Mediator.Send(request);

            return Ok(response);
        }

        [HttpPost]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Games, ActionType = ActionType.Writing, Definition = "Add Game")]
        public async Task<IActionResult> Post([FromQuery] AddGameCommandRequest request)
        {
            request.Files = Request.Form.Files;
            AddGameCommandResponse response = await Mediator.Send(request);
            return Created("Game", response);
        }

        [HttpPut]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Games, ActionType = ActionType.Updating, Definition = "Update Game")]
        public async Task<IActionResult> Put([FromBody] UpdateGameCommandRequest request)
        {
            UpdateGameCommandResponse response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Games, ActionType = ActionType.Deleting, Definition = "Delete Game")]
        public async Task<IActionResult> Delete(string id)
        {
            DeleteGameCommandRequest request = new() { Id = id };
            DeleteGameCommandResponse response = await Mediator.Send(request);
            return Ok(response);
        }
    }
}
