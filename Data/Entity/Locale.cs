using Data.StaticRepository;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data.Entity;

public class Locale
{
    public Locale()
    {
        LocaleTexts = new Dictionary<string, LocaleText>();
    }

    public string DefaultText { get; set; }
    
    [BsonRepresentation(BsonType.String)]
    public LanguageCodeEnum DefaultLanguageCode { get; set; }
    public Dictionary<string, LocaleText> LocaleTexts { get; set; }

    public override string ToString()
    {
        return DefaultText;
    }

    public string ToString(LanguageCodeEnum languageCode)
    {
        var code = languageCode.ToString();
        if (LocaleTexts.ContainsKey(code))
        {
            return LocaleTexts[code].Text;
        }

        return DefaultText;
    }
}