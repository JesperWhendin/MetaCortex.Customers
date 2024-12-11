using MetaCortex.Customers.DataAccess.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MetaCortex.Customers.DataAccess.Repositories;

public abstract class Repository<IEntity, TId>
    : IRepository<IEntity, TId>
    where IEntity : class, IEntity<TId>
{
    private readonly IMongoCollection<IEntity> _collection;

    protected Repository(IMongoClient client, IOptions<MongoDbSettings> options)
    {
        var settings = options.Value;
        var database = client.GetDatabase(settings.DatabaseName);
        _collection = database.GetCollection<IEntity>(settings.CollectionName, new MongoCollectionSettings { AssignIdOnInsert = true });
    }

    public async Task<IEnumerable<IEntity>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<IEntity> GetByIdAsync(TId id)
    {
        return await _collection.Find(e => e.Id.Equals(id)).FirstOrDefaultAsync();
    }

    public async Task<IEntity> AddAsync(IEntity entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
    }
}