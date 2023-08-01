using GameCenterAPI.Application.Repositories.Advertisements;
using GameCenterAPI.Domain.Entities;
using MediatR;

namespace GameCenterAPI.Application.Features.Advertisements.Queries
{
    public class GetByIdAdvertisementQueryRequest : IRequest<GetByIdAdvertisementQueryResponse> 
    {
        public string Id { get; set; }
    }

    public class GetByIdAdvertisementQueryResponse
    {
        public Advertisement Advertisement { get; set; }
    }

    public class GetByIdAdvertisementQueryHandler : IRequestHandler<GetByIdAdvertisementQueryRequest, GetByIdAdvertisementQueryResponse>
    {
        readonly IAdvertisementReadRepository _advertisementReadRepository;

        public GetByIdAdvertisementQueryHandler(IAdvertisementReadRepository advertisementReadRepository)
        {
            _advertisementReadRepository = advertisementReadRepository;
        }

        public async Task<GetByIdAdvertisementQueryResponse> Handle(GetByIdAdvertisementQueryRequest request, CancellationToken cancellationToken)
        {
            Advertisement? advertisement = await _advertisementReadRepository.GetByIdAsync(request.Id);

            if (advertisement == null)
                throw new Exception(ErrorMessages.AdvetisementNotFoundById);

            return new() { Advertisement = advertisement };
        }
    }
}
