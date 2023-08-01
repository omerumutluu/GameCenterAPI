using GameCenterAPI.Application.Repositories.Games;
using GameCenterAPI.Domain.Entities;
using MediatR;

namespace GameCenterAPI.Application.Features.Games.Commands
{
    public class UpdateGameCommandRequest : IRequest<UpdateGameCommandResponse>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }

    public class UpdateGameCommandResponse
    {
        public Game Game { get; set; }
    }

    public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommandRequest, UpdateGameCommandResponse>
    {
        readonly IGameWriteRepository _gameWriteRepository;
        readonly IGameReadRepository _gameReadRepository;

        public UpdateGameCommandHandler(IGameReadRepository gameReadRepository, IGameWriteRepository gameWriteRepository)
        {
            _gameReadRepository = gameReadRepository;
            _gameWriteRepository = gameWriteRepository;
        }

        async Task<UpdateGameCommandResponse> IRequestHandler<UpdateGameCommandRequest, UpdateGameCommandResponse>.Handle(UpdateGameCommandRequest request, CancellationToken cancellationToken)
        {
            Game? game = await _gameReadRepository.GetByIdAsync(request.Id);

            if (game == null)
                throw new Exception(ErrorMessages.GamesNotFoundById);

            if (game.Name == request.Name)
                throw new Exception(ErrorMessages.UpdatedGameNameCanNotBeSameWithOld);

            game.Name = request.Name;
            game.Image = request.Image;

            Game updatedGame = await _gameWriteRepository.UpdateAsync(game);

            return new() { Game = game };
        }
    }
}
