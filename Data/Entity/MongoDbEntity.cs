using Core.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data.Entity;

public abstract class MongoDbEntity : IEntity<Guid>
{
    protected MongoDbEntity()
    {
        Key = Guid.NewGuid();
        CreatedDate = DateTime.UtcNow;
    }
    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId _id { get; set; }
    

    [BsonRepresentation(BsonType.String)]
    public Guid Key { get; set; }
    
    [BsonRepresentation(BsonType.DateTime)]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    [BsonElement(Order = 101)]
    public DateTime CreatedDate { get; set; }
    public bool IsDeleted { get; set; }
}