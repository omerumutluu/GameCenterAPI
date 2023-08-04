using GameCenterAPI.Application.Abstraction.Storage;
using GameCenterAPI.Application.Repositories.Games;
using GameCenterAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GameCenterAPI.Application.Features.Games.Commands
{
    public class UpdateGameCommandRequest : IRequest<UpdateGameCommandResponse>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IFormFileCollection? Files { get; set; }
    }

    public class UpdateGameCommandResponse
    {
        public Game Game { get; set; }
    }

    public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommandRequest, UpdateGameCommandResponse>
    {
        readonly IGameWriteRepository _gameWriteRepository;
        readonly IGameReadRepository _gameReadRepository;
        readonly IAzureStorage _azureStorage;

        public UpdateGameCommandHandler(IGameReadRepository gameReadRepository, IGameWriteRepository gameWriteRepository, IAzureStorage azureStorage)
        {
            _gameReadRepository = gameReadRepository;
            _gameWriteRepository = gameWriteRepository;
            _azureStorage = azureStorage;
        }

        async Task<UpdateGameCommandResponse> IRequestHandler<UpdateGameCommandRequest, UpdateGameCommandResponse>.Handle(UpdateGameCommandRequest request, CancellationToken cancellationToken)
        {
            Game? game = await _gameReadRepository.GetByIdAsync(request.Id);

            if (game == null)
                throw new Exception(ErrorMessages.GamesNotFoundById);

            if (game.Name == request.Name)
                throw new Exception(ErrorMessages.UpdatedGameNameCanNotBeSameWithOld);

            string oldContainerName = game.Image.Split("/")[0];
            string oldFileName = game.Image.Split("/")[1];

            _azureStorage.HasFile(oldContainerName, oldFileName);

            List<(string fileName, string containerName)> result = await _azureStorage.UploadAsync("game-images", request.Files);

            if (result[0].fileName == "" && result[0].containerName == "")
                throw new Exception(ErrorMessages.UnknownErrorWhenImageUpload);

            game.Name = request.Name;
            //game.Image = request.Image;

            Game updatedGame = await _gameWriteRepository.UpdateAsync(game);

            return new() { Game = game };
        }
    }
}
