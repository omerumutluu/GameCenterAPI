using GameCenterAPI.Application.Repositories.Advertisements;
using GameCenterAPI.Domain.Entities;
using MediatR;

namespace GameCenterAPI.Application.Features.Advertisements.Queries
{
    public class GetAllAdvertisementsQueryRequest : IRequest<GetAllAdvertisementsQueryResponse>
    {

    }

    public class GetAllAdvertisementsQueryResponse
    {
        public List<Advertisement> Advertisements { get; set; } = new List<Advertisement>();
        public int Count { get; set; }
    }

    public class GetAllAdvertisementsQueryHandler : IRequestHandler<GetAllAdvertisementsQueryRequest, GetAllAdvertisementsQueryResponse>
    {
        readonly IAdvertisementReadRepository _advertisementReadRepository;

        public GetAllAdvertisementsQueryHandler(IAdvertisementReadRepository advertisementReadRepository)
        {
            _advertisementReadRepository = advertisementReadRepository;
        }

        public Task<GetAllAdvertisementsQueryResponse> Handle(GetAllAdvertisementsQueryRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Advertisement> advertisements = _advertisementReadRepository.GetAll();
            if (advertisements.Count() == 0)
                throw new Exception(ErrorMessages.AdvetisementsNotFound);
            return Task.FromResult(new GetAllAdvertisementsQueryResponse { Advertisements = advertisements.ToList(), Count = advertisements.Count() });
        }
    }
}
