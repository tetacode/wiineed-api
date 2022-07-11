using Data.StaticRepository;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data.Entity;

public class UserSettings
{
    [BsonRepresentation(BsonType.String)]
    public LanguageCodeEnum LanguageCode { get; set; }
}