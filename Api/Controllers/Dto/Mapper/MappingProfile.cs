using AutoMapper;
using Data.Entity;
using Data.Entity.Collection;

namespace Api.Controllers.Dto.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
    }
}