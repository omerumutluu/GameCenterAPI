using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameCenterAPI.API.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        protected readonly IMediator Mediator;

        public BaseController(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
