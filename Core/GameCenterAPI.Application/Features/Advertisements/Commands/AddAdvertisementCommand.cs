using FluentValidation.Results;
using GameCenterAPI.Application.Abstraction.Storage;
using GameCenterAPI.Application.Exceptions;
using GameCenterAPI.Application.Features.Advertisements.Validations;
using GameCenterAPI.Application.Repositories.Advertisements;
using GameCenterAPI.Application.Repositories.Games;
using GameCenterAPI.Domain.Entities;
using GameCenterAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace GameCenterAPI.Application.Features.Advertisements.Commands
{
    public class AddAdvertisementCommandRequest : IRequest<AddAdvertisementCommandResponse>
    {
        public string UserId { get; set; }
        public string GameId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFileCollection Thumbnail { get; set; }
        public IFormFileCollection Medias { get; set; }
        public float Price { get; set; }
        public DateTime ExpiredAt { get; set; }
        public int DeliveryHour { get; set; }
    }
    public class AddAdvertisementCommandResponse
    {
        public Advertisement Advertisement { get; set; }
    }
    public class AddAdvertisementCommandHandler : IRequestHandler<AddAdvertisementCommandRequest, AddAdvertisementCommandResponse>
    {
        readonly IAdvertisementWriteRepository _advertisementWriteRepository;
        readonly IGameReadRepository _gameReadRepository;
        readonly UserManager<AppUser> _userManager;
        readonly IAzureStorage _azureStorage;

        public AddAdvertisementCommandHandler(
            IAdvertisementWriteRepository advertisementWriteRepository,
            IGameReadRepository gameReadRepository,
            UserManager<AppUser> userManager,
            IAzureStorage azureStorage)
        {
            _advertisementWriteRepository = advertisementWriteRepository;
            _gameReadRepository = gameReadRepository;
            _userManager = userManager;
            _azureStorage = azureStorage;
        }

        public async Task<AddAdvertisementCommandResponse> Handle(AddAdvertisementCommandRequest request, CancellationToken cancellationToken)
        {
            AddAdvertisementCommandRequestValidator validator = new();
            ValidationResult result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errors = new List<ValidationExceptionInnerException>();

                foreach (var error in result.Errors)
                    errors.Add(new() { FailedProperty = error.PropertyName, Message = error.ErrorMessage });

                throw new ValidationException(ErrorMessages.ValidationFailed, errors);
            }

            AppUser? user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
                throw new Exception(ErrorMessages.UserCanNotFound);

            Game? game = await _gameReadRepository.GetByIdAsync(request.GameId);

            if (game == null)
                throw new Exception(ErrorMessages.GameCanNotFound);


            List<(string fileName, string containerName)> thumbnailResult = await _azureStorage.UploadAsync("images/advertisements", request.Thumbnail);
            List<(string fileName, string containerName)> mediasResult = await _azureStorage.UploadAsync("images/advertisements", request.Medias);

            Advertisement advertisement = new()
            {
                DeliveryHour = request.DeliveryHour,
                Description = request.Description,
                ExpiredAt = request.ExpiredAt,
                Medias = mediasResult.Select(m => m.containerName + "/" + m.containerName).ToList(),
                Price = request.Price,
                Thumbnail = thumbnailResult[0].fileName,
                Title = request.Title,
                Game = game,
                User = user
            };

            Advertisement? addedAdvertisement = await _advertisementWriteRepository.AddAsync(advertisement);

            if (addedAdvertisement == null)
                throw new Exception(ErrorMessages.UnknownErrorWhenAdvetisementAdded);

            return new() { Advertisement = advertisement };
        }
    }
}
