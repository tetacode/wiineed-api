using Data.StaticRepository;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data.Entity;

public class LocaleText
{
    public string Text { get; set; }
    
    [BsonRepresentation(BsonType.String)]
    public LanguageCodeEnum LanguageCode { get; set; }
    public Boolean IsTranslated { get; set; }

    public override string ToString()
    {
        return Text;
    }
}