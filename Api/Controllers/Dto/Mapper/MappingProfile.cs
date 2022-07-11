using AutoMapper;
using Data.Entity;
using Data.Entity.Collection;
using Service.Service;

namespace Api.Controllers.Dto.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserSettings, UserSettingsDto>();
    }
}