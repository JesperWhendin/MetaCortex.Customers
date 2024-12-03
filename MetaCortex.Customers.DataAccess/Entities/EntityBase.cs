using MetaCortex.Customers.DataAccess.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MetaCortex.Customers.DataAccess.Entities;

public class EntityBase : IEntity<string>
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } 
}
