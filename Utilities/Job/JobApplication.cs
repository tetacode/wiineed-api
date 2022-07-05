using Microsoft.Extensions.Logging;
using System.Timers;

namespace Utilities.Job;

public abstract class JobApplication : IJobApplication
{
    private List<JobTimer> _taskList = new List<JobTimer>();

    public abstract void InitJobLogger();
    
    protected JobApplication()
    {
        InitJobLogger();
    }

    public void AddTask(JobTimerTask timerTask)
    {
        var timerJob = new JobTimer(timerTask, GetLoopSecond());
        _taskList.Add(timerJob);
    }

    public abstract int GetLoopSecond();

    public void Run()
    {
        _taskList.ForEach(t =>
        {
            t.Start();
        });
        
        while (true)
        {
            Thread.Sleep(1000);
        }
    }
}