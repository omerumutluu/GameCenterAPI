using GameCenterAPI.Application.Repositories;
using GameCenterAPI.Domain.Entities.Base;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace GameCenterAPI.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly IConfiguration _configuration;
        public ReadRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IMongoCollection<T> Collection => new MongoClient(_configuration.GetConnectionString("MongoDb")).GetDatabase("game-center-db").GetCollection<T>(typeof(T).Name.ToLowerInvariant() + "s");

        public IQueryable<T> GetAll(Expression<Func<T, bool>>? predicate = null)
            => predicate != null
                ? Collection.AsQueryable().Where(predicate)
                : Collection.AsQueryable();


        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
            => await Collection.Find(predicate).FirstOrDefaultAsync();


        public async Task<T> GetByIdAsync(string id)
            => await Collection.Find(entity => entity.Id == id).FirstOrDefaultAsync();
    }
}
