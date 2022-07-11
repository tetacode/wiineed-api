using Api.Controllers.Dto.Mapper;
using Data.StaticRepository;

namespace Api.Controllers.Dto;

public class UserSettingsDto : BaseDto
{
    public LanguageCodeEnum LanguageCode { get; set; }
}