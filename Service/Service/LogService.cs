using System.Text.Json;
using Core.Service.Exception;
using Data.Entity;
using Data.Entity.Collection;
using Data.Repository.Abstract;
using Service.Service.Abstract;

namespace Service.Service;

public class LogService : ILogService
{
    private readonly ILogRepository _logRepository;
    private readonly User _user;

    public LogService(ILogRepository logRepository, User user)
    {
        _logRepository = logRepository;
        _user = user;
    }

    public void CreateLog(Exception exp)
    {
        var log = new
        {
            Message = exp?.Message,
            StackTrace = exp?.StackTrace,
            InnerException = new
            {
                Message = exp?.InnerException?.Message,
                StackTrace = exp?.InnerException?.StackTrace,
            }
        };
        _logRepository.Add(new Log()
        {
            Data = JsonSerializer.Serialize(log),
            UserId = _user != null ? _user.Key : null
        });
    }

    public void CreateLog(ServiceException exp)
    {
        var log = new
        {
            Message = new
            {
                Text = exp?.GetServiceMessage(),
                ErrorData = exp?.GetErrorData()
            },
            StackTrace = exp?.StackTrace,
        };
        _logRepository.Add(new Log()
        {
            Data = JsonSerializer.Serialize(log),
            UserId = _user != null ? _user.Key : null
        });
    }

    public void CreateLog(ServiceNotAllowedException exp)
    {
        var log = new
        {
            Message = new
            {
                Text = exp?.GetServiceMessage(),
                ErrorData = exp?.GetErrorData()
            },
            StackTrace = exp?.StackTrace,
        };
        _logRepository.Add(new Log()
        {
            Data = JsonSerializer.Serialize(log),
            UserId = _user != null ? _user.Key : null
        });
    }
}