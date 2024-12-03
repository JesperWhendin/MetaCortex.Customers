using MetaCortex.Customers.DataAccess.Entities;
using MetaCortex.Customers.DataAccess.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MetaCortex.Customers.DataAccess.Repositories;

public class CustomerRepository : Repository<Customer, string>, ICustomerRepository
{
    private readonly IMongoCollection<Customer> _collection;

    public CustomerRepository(IMongoClient client, IOptions<MongoDbSettings> options) : base(client, options)
    {
        var database = client.GetDatabase(options.Value.DatabaseName);
        _collection = database.GetCollection<Customer>(options.Value.CollectionName, new MongoCollectionSettings { AssignIdOnInsert = true} );
    }

    public async Task<Customer> GetByEmailAsync(string email)
    {
        return await _collection.Find(e => e.Email == email).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(Customer customer)
    {
        var filter = Builders<Customer>.Filter.Eq(e => e.Id, customer.Id);
        await _collection.ReplaceOneAsync(filter, customer);
    }

    public async Task DeleteAsync(string id)
    {
        var filter = Builders<Customer>.Filter.Eq(e => e.Id, id);
        await _collection.DeleteOneAsync(filter);
    }
}
