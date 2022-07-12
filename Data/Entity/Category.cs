using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data.Entity;

public class Category
{
    public Category()
    {
        Id = Guid.NewGuid();
        Image = new Media();
    }

    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public Locale Name { get; set; }
    public Locale Description { get; set; }
    public Media Image { get; set; }
    
    public List<Guid> Products { get; set; }

}