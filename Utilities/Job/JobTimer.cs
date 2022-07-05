using Microsoft.Extensions.Logging;

namespace Utilities.Job;

public class JobTimer
{
    private System.Timers.Timer _timer;
    private JobTimerTask _timerTask;

    public JobTimer(JobTimerTask timerTask, int second)
    {
        _timerTask = timerTask;
        _timer = new System.Timers.Timer(second * 1000);
        _timer.Elapsed += ((sender, args) =>
        {
            if (timerTask.IsCompleted())
            {
                try
                {
                    timerTask.SetIsCompleted(false);
                
                    // JobLogger.LogInfo(_timerTask.ToString(),$"Task Start");
                    _timerTask.Run();
                    // JobLogger.LogInfo(_timerTask.ToString(),$"Task Completed");
                }
                catch (Exception e)
                {
                    JobLogger.LogError(_timerTask.ToString(),$"Error: {e.Message} -> {e.StackTrace}");
                }
                timerTask.SetIsCompleted(true);
            }
        });
    }

    public void Start()
    {
        _timer.Enabled = true;
    }
}