using AutoMapper;
using Data.Entity;

namespace Api.Controllers.Dto.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
    }
}