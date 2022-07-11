using Core.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data.Entity;

public abstract class MongoDbEntity : IEntity<Guid>
{
    protected MongoDbEntity()
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTime.UtcNow;
    }

    [BsonRepresentation(BsonType.String)]
    [BsonId]
    [BsonElement("_id")]
    public Guid Id { get; set; }
    
    [BsonRepresentation(BsonType.DateTime)]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    [BsonElement(Order = 101)]
    public DateTime CreatedDate { get; set; }
    public bool IsDeleted { get; set; }
}