using GameCenterAPI.Application.Repositories.Games;
using GameCenterAPI.Domain.Entities;
using MediatR;

namespace GameCenterAPI.Application.Features.Games.Queries
{
    public class GetAllGamesQueryRequest : IRequest<GetAllGamesQueryResponse>
    {
    }

    public class GetAllGamesQueryResponse
    {
        public List<Game> Games { get; set; } = new List<Game>();
        public int Count { get; set; }
    }

    public class GetAllGamesQueryHandler : IRequestHandler<GetAllGamesQueryRequest, GetAllGamesQueryResponse>
    {
        readonly IGameReadRepository _gameReadRepository;

        public GetAllGamesQueryHandler(IGameReadRepository gameReadRepository)
        {
            _gameReadRepository = gameReadRepository;
        }

        public async Task<GetAllGamesQueryResponse> Handle(GetAllGamesQueryRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Game> games = _gameReadRepository.GetAll();
            if (games.Count() == 0)
                throw new Exception(ErrorMessages.GamesNotFound);
            return new() { Games = games.ToList(), Count = games.Count() };
        }
    }
}
