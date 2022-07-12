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
        CreateMap<Business, BusinessDto>();
        CreateMap<Address, AddressDto>();
        CreateMap<Media, MediaDto>();
        CreateMap<SocialMedia, SocialMediaDto>();
        CreateMap<Geolocation, GeolocationDto>();
        CreateMap<Menu, MenuDto>();
        CreateMap<Category, CategoryDto>();
        CreateMap<WorkingHour, WorkingHourDto>();
        CreateMap<CategoryItem, CategoryItemDto>();
    }
}