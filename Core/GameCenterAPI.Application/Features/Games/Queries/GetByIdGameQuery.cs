using GameCenterAPI.Application.Repositories.Games;
using GameCenterAPI.Domain.Entities;
using MediatR;

namespace GameCenterAPI.Application.Features.Games.Queries
{
    public class GetByIdGameQueryRequest : IRequest<GetByIdGameQueryResponse>
    {
        public string Id { get; set; }
    }

    public class GetByIdGameQueryResponse
    {
        public Game Game { get; set; }
    }

    public class GetByIdGameQueryHandler : IRequestHandler<GetByIdGameQueryRequest, GetByIdGameQueryResponse>
    {
        readonly IGameReadRepository _gameReadRepository;

        public GetByIdGameQueryHandler(IGameReadRepository gameReadRepository)
        {
            _gameReadRepository = gameReadRepository;
        }

        public async Task<GetByIdGameQueryResponse> Handle(GetByIdGameQueryRequest request, CancellationToken cancellationToken)
        {
            Game? game = await _gameReadRepository.GetByIdAsync(request.Id);

            if (game == null)
                throw new Exception(ErrorMessages.GamesNotFoundById);

            return new() { Game = game };
        }
    }
}
