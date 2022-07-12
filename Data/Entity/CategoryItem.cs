using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data.Entity;

public class CategoryItem
{
    public CategoryItem()
    {
        Key = Guid.NewGuid();
        Order = 0;
    }

    [BsonRepresentation(BsonType.String)]
    public Guid Key { get; set; }
    public int Order { get; set; }
    
    public Guid ProductId { get; set; }
}