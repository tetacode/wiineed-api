using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data.Entity;

public class Menu
{
    public Menu()
    {
        Id = Guid.NewGuid();
        Enabled = true;
        Categories = new List<Category>();
    }

    [BsonElement("_id")]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Boolean Enabled { get; set; }
    public List<Category> Categories { get; set; }
}