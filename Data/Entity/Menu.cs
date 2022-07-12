using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data.Entity;

public class Menu
{
    public Menu()
    {
        Key = Guid.NewGuid();
        Enabled = true;
        Categories = new List<Category>();
    }

    [BsonRepresentation(BsonType.String)]
    public Guid Key { get; set; }
    public string Name { get; set; }
    public Boolean Enabled { get; set; }
    public List<Category> Categories { get; set; }
}