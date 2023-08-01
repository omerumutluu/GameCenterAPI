using GameCenterAPI.Domain.Entities.Base;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace GameCenterAPI.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task<T> UpdateAsync(string id, T entity);
        Task<T> UpdateAsync(T entity);
        Task<DeleteResult> DeleteAsync(T entity);
        Task<DeleteResult> DeleteAsync(string id);
        Task<DeleteResult> DeleteAll(Expression<Func<T, bool>> method);
    }
}
