using Data.StaticRepository;

namespace Api.Controllers.Dto;

public class WorkingHourDto
{
    public Guid Key { get; set; }
    public DayEnum Day { get; set; }
    public TimeOnly OpeningTime { get; set; }
    public TimeOnly ClosingTime { get; set; }
}