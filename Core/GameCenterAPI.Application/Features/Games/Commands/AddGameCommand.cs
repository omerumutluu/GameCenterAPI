using GameCenterAPI.Application.Repositories.Games;
using GameCenterAPI.Domain.Entities;
using MediatR;

namespace GameCenterAPI.Application.Features.Games.Commands
{
    public class AddGameCommandRequest : IRequest<AddGameCommandResponse>
    {
        public string Name { get; set; }
        public string Image { get; set; }
    }

    public class AddGameCommandResponse
    {
        public Game Game { get; set; }
    }

    public class AddGameCommandHandler : IRequestHandler<AddGameCommandRequest, AddGameCommandResponse>
    {
        readonly IGameWriteRepository _gameWriteRepository;
        readonly IGameReadRepository _gameReadRepository;

        public AddGameCommandHandler(IGameReadRepository gameReadRepository, IGameWriteRepository gameWriteRepository)
        {
            _gameReadRepository = gameReadRepository;
            _gameWriteRepository = gameWriteRepository;
        }

        public async Task<AddGameCommandResponse> Handle(AddGameCommandRequest request, CancellationToken cancellationToken)
        {
            Game? game = await _gameReadRepository.GetAsync(x => x.Name == request.Name);

            if (game != null)
                throw new Exception(ErrorMessages.GameNameAlreadyExist);

            Game? addedGame = await _gameWriteRepository.AddAsync(new() { Name = request.Name, Image = request.Image });

            if (addedGame == null)
                throw new Exception(ErrorMessages.UnknownErrorWhenGameAdded);

            return new() { Game = addedGame };

        }
    }
}
