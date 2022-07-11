using Data.StaticRepository;
using MongoDB.Bson.Serialization.Attributes;

namespace Data.Entity;

public class WorkingHour
{
    public WorkingHour()
    {
        Id = Guid.NewGuid();
    }

    [BsonElement("_id")]
    public Guid Id { get; set; }
    public DayEnum Day { get; set; }
    public TimeOnly OpeningTime { get; set; }
    public TimeOnly ClosingTime { get; set; }
}