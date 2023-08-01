using GameCenterAPI.Application.Repositories.Advertisements;
using GameCenterAPI.Domain.Entities;
using MediatR;
using MongoDB.Driver;

namespace GameCenterAPI.Application.Features.Advertisements.Commands
{
    public class DeleteAdvertisementCommandRequest : IRequest<DeleteAdvertisementCommandResponse>
    {
        public string Id { get; set; }
    }

    public class DeleteAdvertisementCommandResponse
    {
        public Advertisement Advertisement { get; set; }
    }

    public class DeleteAdvertisementCommandHandler : IRequestHandler<DeleteAdvertisementCommandRequest, DeleteAdvertisementCommandResponse>
    {
        readonly IAdvertisementReadRepository _advertisementReadRepository;
        readonly IAdvertisementWriteRepository _advertisementWriteRepository;

        public DeleteAdvertisementCommandHandler(IAdvertisementReadRepository advertisementReadRepository, IAdvertisementWriteRepository advertisementWriteRepository)
        {
            _advertisementReadRepository = advertisementReadRepository;
            _advertisementWriteRepository = advertisementWriteRepository;
        }

        public async Task<DeleteAdvertisementCommandResponse> Handle(DeleteAdvertisementCommandRequest request, CancellationToken cancellationToken)
        {
            Advertisement? advertisement = await _advertisementReadRepository.GetByIdAsync(request.Id);

            if (advertisement == null)
                throw new Exception(ErrorMessages.AdvetisementNotFoundById);

            DeleteResult deleteResult = await _advertisementWriteRepository.DeleteAsync(advertisement);

            if (deleteResult.DeletedCount == 0)
                throw new Exception(ErrorMessages.UnknownErrorWhenAdvetisementDeleted);

            return new() { Advertisement = advertisement };
        }
    }
}
