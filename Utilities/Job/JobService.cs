namespace Utilities.Job;

public abstract class JobService
{
    public string? EventId { get; set; }
    
    public string? Name { get; set; }

    public string GetEventId()
    {
        return $"{Name}({EventId})";
    }
}