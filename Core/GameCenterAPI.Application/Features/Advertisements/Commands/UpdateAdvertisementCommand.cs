using GameCenterAPI.Application.Repositories.Advertisements;
using GameCenterAPI.Application.Repositories.Games;
using GameCenterAPI.Domain.Entities;
using MediatR;

namespace GameCenterAPI.Application.Features.Advertisements.Commands
{
    public class UpdateAdvertisementCommandRequest : IRequest<UpdateAdvertisementCommandResponse>
    {
        public string Id { get; set; }
        public string GameId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public List<string> Medias { get; set; } = new List<string>();
        public float Price { get; set; }
        public DateTime ExpiredAt { get; set; }
        public int DeliveryHour { get; set; }
    }

    public class UpdateAdvertisementCommandResponse
    {
        public Advertisement Advertisement { get; set; }
    }

    public class UpdateAdvertisementCommandHandler : IRequestHandler<UpdateAdvertisementCommandRequest, UpdateAdvertisementCommandResponse>
    {
        readonly IAdvertisementReadRepository _advertisementReadRepository;
        readonly IAdvertisementWriteRepository _advertisementWriteRepository;
        readonly IGameReadRepository _gameReadRepository;

        public UpdateAdvertisementCommandHandler(IAdvertisementWriteRepository advertisementWriteRepository, IAdvertisementReadRepository advertisementReadRepository, IGameReadRepository gameReadRepository)
        {
            _advertisementWriteRepository = advertisementWriteRepository;
            _advertisementReadRepository = advertisementReadRepository;
            _gameReadRepository = gameReadRepository;
        }

        public async Task<UpdateAdvertisementCommandResponse> Handle(UpdateAdvertisementCommandRequest request, CancellationToken cancellationToken)
        {
            Advertisement? advertisement = await _advertisementReadRepository.GetByIdAsync(request.Id);

            if (advertisement == null)
                throw new Exception(ErrorMessages.AdvetisementNotFoundById);

            Game? game = await _gameReadRepository.GetByIdAsync(request.GameId);

            if (game == null)
                throw new Exception(ErrorMessages.GameCanNotFound);

            advertisement.DeliveryHour = request.DeliveryHour;
            advertisement.Game = game;
            advertisement.Price = request.Price;
            advertisement.Title = request.Title;
            advertisement.Description = request.Description;
            advertisement.Thumbnail = request.Thumbnail;
            advertisement.Medias = request.Medias;

            Advertisement? updatedAdvertisement = await _advertisementWriteRepository.UpdateAsync(advertisement);

            return new() { Advertisement = updatedAdvertisement };
        }
    }
}
