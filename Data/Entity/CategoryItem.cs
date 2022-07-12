using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data.Entity;

public class CategoryItem
{
    public CategoryItem()
    {
        Id = Guid.NewGuid();
        Order = 0;
    }

    [BsonElement("_id")]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public int Order { get; set; }
    
    public Guid ProductId { get; set; }
}