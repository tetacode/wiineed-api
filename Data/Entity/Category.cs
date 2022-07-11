using MongoDB.Bson.Serialization.Attributes;

namespace Data.Entity;

public class Category
{
    public Category()
    {
        Id = Guid.NewGuid();
        Image = new Media();
    }

    [BsonElement("_id")]
    public Guid Id { get; set; }
    public Locale Name { get; set; }
    public Media Image { get; set; }
    
}