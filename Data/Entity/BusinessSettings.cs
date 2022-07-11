using Data.StaticRepository;

namespace Data.Entity;

public class BusinessSettings
{
    public BusinessSettings()
    {
        DefaultLanguageCode = LanguageCodeEnum.EN;
        Currency = CurrencyEnum.USD;
    }

    public string Key { get; set; }
    public LanguageCodeEnum DefaultLanguageCode { get; set; }
    public CurrencyEnum Currency { get; set; }
}