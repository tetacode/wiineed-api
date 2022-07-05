using Core.Repository.Abstract;
using Core.Repository.Model;
using Core.Service.Result.Abstract;

namespace Core.Service;

public class DataGridInput<T> where T : new()
{
    public PaginationQuery? PaginationQuery { get; set; }
    public List<OrderByItem>? OrderByItems { get; set; }
    public T? Filter { get; set; }
}