namespace Core.Service.Result.Abstract;

public interface IDataResult
{
    public string Message { get; set; }
    
    public object ErrorData { get; set; }
}