using Data.StaticRepository;

namespace Service.Model.DataInput;

public class WorkingHourInput
{
    public Guid Key { get; set; }
    
    public DayEnum Day { get; set; }
    public DateTime OpeningTime { get; set; }
    public DateTime ClosingTime { get; set; }
}