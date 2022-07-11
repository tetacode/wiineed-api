using Data.StaticRepository;

namespace Data.Entity;

public class Locale
{
    public Locale()
    {
        LocaleTexts = new Dictionary<LanguageCodeEnum, LocaleText>();
        DefaultLanguageCode = LanguageCodeEnum.NULL;
    }

    public string DefaultText { get; set; }
    
    public LanguageCodeEnum DefaultLanguageCode { get; set; }
    public Dictionary<LanguageCodeEnum, LocaleText> LocaleTexts { get; set; }

    public override string ToString()
    {
        return DefaultText;
    }

    public string ToString(LanguageCodeEnum languageCode)
    {
        if (LocaleTexts.ContainsKey(languageCode))
        {
            return LocaleTexts[languageCode].Text;
        }

        return DefaultText;
    }
}