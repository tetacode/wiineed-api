using Core.Service.Result.Abstract;

namespace Core.Service.Result;

public class DataListResult<T> : IDataResult
{
    public string? Message { get; set; }
    public object? ErrorData { get; set; }
    public IEnumerable<T> Data { get; set;  }

    public DataListResult(IEnumerable<T> data)
    {
        Data = data;
    }
}