using Api.Controllers.Dto.Mapper;
using Data.Entity;

namespace Api.Controllers.Dto;

public class BusinessDto : BaseDto
{
    public Locale Name { get; set; }
    public Locale Description { get; set; }
}