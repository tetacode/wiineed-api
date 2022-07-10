using Core.Repository.Abstract;
using Core.Repository.Model;
using Core.Service.Result.Abstract;

namespace Core.Service;

public class DataGridInput
{
    public PaginationQuery? PaginationQuery { get; set; }
    public List<OrderByItem>? OrderByItems { get; set; }
}

public class DataGridInput<T> : DataGridInput where T : new()
{
    public T? Filter { get; set; }
}