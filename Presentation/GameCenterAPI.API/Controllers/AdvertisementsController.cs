using GameCenterAPI.API.Controllers.Base;
using GameCenterAPI.Application.Features.Advertisements.Commands;
using GameCenterAPI.Application.Features.Advertisements.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameCenterAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementsController : BaseController
    {
        public AdvertisementsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            GetAllAdvertisementsQueryResponse response = await Mediator.Send(new GetAllAdvertisementsQueryRequest());
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            GetByIdAdvertisementQueryRequest request = new() { Id = id };
            GetByIdAdvertisementQueryResponse response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddAdvertisementCommandRequest request)
        {
            AddAdvertisementCommandResponse response = await Mediator.Send(request);
            return Created("Advertisement", response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateAdvertisementCommandRequest request)
        {
            UpdateAdvertisementCommandResponse response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            DeleteAdvertisementCommandRequest request = new() { Id = id };
            DeleteAdvertisementCommandResponse response = await Mediator.Send(request);
            return Ok(response);
        }
    }
}
