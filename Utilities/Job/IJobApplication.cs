namespace Utilities.Job;

public interface IJobApplication
{
    public int GetLoopSecond();
    public void Run();
}