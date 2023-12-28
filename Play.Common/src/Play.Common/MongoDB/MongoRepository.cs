using System.Linq.Expressions;
using MongoDB.Driver;
using Play.Common;

namespace Play.Common.MongoDB;

public class MongoRepository<T> : IRepository<T> where T : IEntity
{
    private readonly IMongoCollection<T> _dbCollection;

    public MongoRepository(IMongoDatabase database, string collectionName)
    {
        _dbCollection = database.GetCollection<T>(collectionName);
    }
    public async Task<List<T>> GetAllAsync()
    {
        return await _dbCollection.Find(_ => true).ToListAsync();
    }
    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter)
    {
        return await _dbCollection.Find(filter).ToListAsync();
    }
    public async Task<T?> GetAsync(string id)
    {
        return await _dbCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }
    public async Task<T?> GetAsync(Expression<Func<T, bool>> filter)
    {
        return await _dbCollection.Find(filter).FirstOrDefaultAsync();
    }
    public async Task CreateAsync(T entity)
    {
        await _dbCollection.InsertOneAsync(entity);
    }
    public async Task UpdateAsync(string id, T entity)
    {
        await _dbCollection.ReplaceOneAsync(x => x.Id == id, entity);
    }
    public async Task RemoveAsync(string id)
    {
        await _dbCollection.DeleteOneAsync(x => x.Id == id);
    }
}
