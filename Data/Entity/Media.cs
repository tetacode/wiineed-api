using Data.StaticRepository;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data.Entity;

public class Media
{
    public Media()
    {
        Key = Guid.NewGuid();
        MediaType = MediaTypeEnum.IMAGE;
        CreatedDate = DateTime.UtcNow;
        Enabled = true;
        Order = 1;
    }

    [BsonRepresentation(BsonType.String)]
    public Guid Key { get; set; }
    
    public string Path { get; set; }
    public string Name { get; set; }
    public string OriginalName { get; set; }
    public string Src { get; set; }
    public string ThumbnailSrc { get; set; }
    public int Order { get; set; }
    
    [BsonRepresentation(BsonType.String)]
    public MediaTypeEnum MediaType { get; set; }
    public DateTime CreatedDate { get; set; }
    public Boolean Enabled { get; set; }
}