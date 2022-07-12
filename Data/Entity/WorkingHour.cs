using Data.StaticRepository;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data.Entity;

public class WorkingHour
{
    public WorkingHour()
    {
        Key = Guid.NewGuid();
    }

    [BsonRepresentation(BsonType.String)]
    public Guid Key { get; set; }
    
    [BsonRepresentation(BsonType.String)]
    public DayEnum Day { get; set; }
    public TimeOnly OpeningTime { get; set; }
    public TimeOnly ClosingTime { get; set; }
}