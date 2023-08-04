using GameCenterAPI.Application.Abstraction.Storage;
using GameCenterAPI.Application.Repositories.Games;
using GameCenterAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GameCenterAPI.Application.Features.Games.Commands
{
    public class AddGameCommandRequest : IRequest<AddGameCommandResponse>
    {
        public string Name { get; set; }
        public IFormFileCollection? Files { get; set; }
    }

    public class AddGameCommandResponse
    {
        public Game Game { get; set; }
    }

    public class AddGameCommandHandler : IRequestHandler<AddGameCommandRequest, AddGameCommandResponse>
    {
        readonly IGameWriteRepository _gameWriteRepository;
        readonly IGameReadRepository _gameReadRepository;
        readonly IAzureStorage _azureStorage;

        public AddGameCommandHandler(IGameReadRepository gameReadRepository, IGameWriteRepository gameWriteRepository, IAzureStorage azureStorage)
        {
            _gameReadRepository = gameReadRepository;
            _gameWriteRepository = gameWriteRepository;
            _azureStorage = azureStorage;
        }

        public async Task<AddGameCommandResponse> Handle(AddGameCommandRequest request, CancellationToken cancellationToken)
        {
            Game? game = await _gameReadRepository.GetAsync(x => x.Name == request.Name);

            if (game != null)
                throw new Exception(ErrorMessages.GameNameAlreadyExist);

            List<(string fileName, string containerName)> result = await _azureStorage.UploadAsync("game-images", request.Files);

            if (result[0].fileName == "" && result[0].containerName == "")
                throw new Exception(ErrorMessages.UnknownErrorWhenImageUpload);

            string fileName = $"/{result[0].containerName}/{result[0].fileName}";

            Game? addedGame = await _gameWriteRepository.AddAsync(new() { Name = request.Name, Image = fileName });

            if (addedGame == null)
                throw new Exception(ErrorMessages.UnknownErrorWhenGameAdded);

            return new() { Game = addedGame };
        }
    }
}
