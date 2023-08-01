using GameCenterAPI.Domain.Entities.Base;
using MongoDB.Driver;

namespace GameCenterAPI.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        IMongoCollection<T> Collection { get; }
    }
}
