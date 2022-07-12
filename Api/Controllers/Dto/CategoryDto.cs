using Data.Entity;

namespace Api.Controllers.Dto;

public class CategoryDto
{
    public Guid Key { get; set; }
    public Locale Name { get; set; }
    public Locale Description { get; set; }
    public MediaDto Image { get; set; }
}