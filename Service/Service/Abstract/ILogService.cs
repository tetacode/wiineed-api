using Core.Service.Exception;

namespace Service.Service.Abstract;

public interface ILogService
{
    public void CreateLog(Exception exp);
    public void CreateLog(ServiceException exp);
    public void CreateLog(ServiceNotAllowedException exp);
}