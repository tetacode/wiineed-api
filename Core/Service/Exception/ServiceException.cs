namespace Core.Service.Exception;

public class ServiceException : System.Exception
{
    public bool CreateLog { get; set; } = true;
    private string _message;
    private object _errorData;

    public ServiceException(string message, object errorData = null)
    {
        this._message = message;
        this._errorData = errorData;
    }

    public ServiceException(string message, object errorData, bool createLog)
    {
        this._message = message;
        this._errorData = errorData;
        CreateLog = createLog;
    }

    public string GetServiceMessage()
    {
        return _message;
    }

    public object GetErrorData()
    {
        return _errorData;
    }
}