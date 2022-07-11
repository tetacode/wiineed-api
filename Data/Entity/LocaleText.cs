using Data.StaticRepository;

namespace Data.Entity;

public class LocaleText
{
    public string Text { get; set; }
    public LanguageCodeEnum LanguageCode { get; set; }
    public Boolean IsTranslated { get; set; }

    public override string ToString()
    {
        return Text;
    }
}