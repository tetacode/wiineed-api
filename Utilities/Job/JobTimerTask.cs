namespace Utilities.Job;

public abstract class JobTimerTask
{
    private bool _isCompleted = true;
    private string _eventId = Guid.NewGuid().ToString();
    private List<JobService> _services = new List<JobService>();

    public void AddJobService(JobService jobService)
    {
        _services.Add(jobService);
    }
    public void SetIsCompleted(bool isCompleted)
    {
        if (!isCompleted)
        {
            _eventId = Guid.NewGuid().ToString();
            OnChangeEventId(_eventId);
            _services.ForEach(x =>
            {
                x.EventId = _eventId;
                x.Name = GetTaskName();
            });
        }
        
        _isCompleted = isCompleted;
    }

    public bool IsCompleted()
    {
        return _isCompleted;
    }
    public string EventId()
    {
        return _eventId;
    }

    public override string ToString()
    {
        return $"{GetTaskName()}({EventId()})";
    }

    public abstract void OnChangeEventId(string eventId);
    
    public abstract string GetTaskName();
    public abstract void Run();
}