using GameCenterAPI.Application.Repositories.Games;
using GameCenterAPI.Domain.Entities;
using MediatR;
using MongoDB.Driver;

namespace GameCenterAPI.Application.Features.Games.Commands
{
    public class DeleteGameCommandRequest : IRequest<DeleteGameCommandResponse>
    {
        public string Id { get; set; }
    }

    public class DeleteGameCommandResponse
    {
        public Game Game { get; set; }
    }

    public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommandRequest, DeleteGameCommandResponse>
    {
        readonly IGameReadRepository _gameReadRepository;
        readonly IGameWriteRepository _gameWriteRepository;

        public DeleteGameCommandHandler(IGameWriteRepository gameWriteRepository, IGameReadRepository gameReadRepository)
        {
            _gameWriteRepository = gameWriteRepository;
            _gameReadRepository = gameReadRepository;
        }

        public async Task<DeleteGameCommandResponse> Handle(DeleteGameCommandRequest request, CancellationToken cancellationToken)
        {
            Game? game = await _gameReadRepository.GetByIdAsync(request.Id);

            if (game == null)
                throw new Exception(ErrorMessages.GamesNotFoundById);

            DeleteResult deleteResult = await _gameWriteRepository.DeleteAsync(game);

            if (deleteResult.DeletedCount == 0)
                throw new Exception(ErrorMessages.UnknownErrorWhenGameDeleted);

            return new() { Game = game };
        }
    }

}
