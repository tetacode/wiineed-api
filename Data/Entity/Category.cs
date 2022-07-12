using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data.Entity;

public class Category
{
    public Category()
    {
        Key = Guid.NewGuid();
        Image = new Media();
    }

    [BsonRepresentation(BsonType.String)]
    public Guid Key { get; set; }
    public Locale Name { get; set; }
    public Locale Description { get; set; }
    public Media Image { get; set; }
    
    public List<Guid> Products { get; set; }

}