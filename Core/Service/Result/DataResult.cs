using Core.Service.Result.Abstract;

namespace Core.Service.Result;

public class DataResult<T> : IDataResult
{
    public DataResult(T data)
    {
        Data = data;
    }

    public T Data { get; set; }

    public string Message { get; set; }
    public object ErrorData { get; set; }
}