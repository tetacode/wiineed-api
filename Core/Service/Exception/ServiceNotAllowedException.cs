namespace Core.Service.Exception;

public class ServiceNotAllowedException : System.Exception
{
    private string _message;
    private object _errorData;

    public ServiceNotAllowedException(string message, object errorData = null)
    {
        this._message = message;
        this._errorData = errorData;
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