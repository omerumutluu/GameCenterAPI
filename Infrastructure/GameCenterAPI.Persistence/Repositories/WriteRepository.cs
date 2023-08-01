using GameCenterAPI.Application.Repositories;
using GameCenterAPI.Domain.Entities.Base;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace GameCenterAPI.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly IConfiguration _configuration;
        public WriteRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IMongoCollection<T> Collection => new MongoClient(_configuration.GetConnectionString("MongoDb")).GetDatabase("game-center-db").GetCollection<T>(typeof(T).Name.ToLowerInvariant() + "s");

        public async Task<T> AddAsync(T entity)
        {
            await Collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            List<T> results = new();
            foreach (var entity in entities)
            {
                results.Add(await AddAsync(entity));
            }
            return results;
        }

        public async Task<DeleteResult> DeleteAll(Expression<Func<T, bool>> method)
        {
            return await Collection.DeleteManyAsync(method);
        }

        public async Task<DeleteResult> DeleteAsync(T entity)
        {
            return await Collection.DeleteOneAsync(ent => ent.Id == entity.Id);
        }

        public async Task<DeleteResult> DeleteAsync(string id)
        {
            return await Collection.DeleteOneAsync(entity => entity.Id == id);
        }

        public async Task<T> UpdateAsync(string id, T entity)
        {
            return await Collection.FindOneAndReplaceAsync(entity => entity.Id == id, entity);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            return await Collection.FindOneAndReplaceAsync(ent => ent.Id == entity.Id, entity);
        }
    }
}
