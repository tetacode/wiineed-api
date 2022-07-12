using Data.Entity;
using Data.StaticRepository;

namespace Service.Model.DataInput;

public class LocaleInput
{
    public string DefaultText { get; set; }
    
    public Dictionary<string, LocaleText> LocaleTexts { get; set; }

    public Locale ToLocale(LanguageCodeEnum languageCode)
    {
        return new Locale()
        {
            DefaultText = DefaultText,
            DefaultLanguageCode = languageCode,
            LocaleTexts = LocaleTexts
        };
    }
}