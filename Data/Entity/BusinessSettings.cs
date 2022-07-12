using Data.StaticRepository;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data.Entity;

public class BusinessSettings
{
    public BusinessSettings()
    {
        DefaultLanguageCode = LanguageCodeEnum.EN;
        Currency = CurrencyEnum.USD;
    }

    public string Key { get; set; }
    
    [BsonRepresentation(BsonType.String)]
    public LanguageCodeEnum DefaultLanguageCode { get; set; }
    
    
    [BsonRepresentation(BsonType.String)]
    public CurrencyEnum Currency { get; set; }
}