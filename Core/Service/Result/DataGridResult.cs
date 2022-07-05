namespace Core.Service.Result;

public class Pagination
{
    public int PageNumber { get; set; }
    public int PageSize { get; set;}
    public int TotalPages { get; set;}
    public int TotalRecords { get; set;}
}
    
public class DataGridResult<T> : DataListResult<T>
{
    public Pagination Pagination { get; }

    public DataGridResult(IEnumerable<T> data, int pageNumber, int pageSize,int totalRecords) : base(data)
    {
        Pagination = new Pagination()
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalRecords = totalRecords,
            TotalPages = Convert.ToInt32(Math.Ceiling(((double)totalRecords/(double)pageSize)))
        };
    }

    public DataGridResult<TNew> CopyFrom<TNew>(IEnumerable<TNew> data)
    {
        var grid = new DataGridResult<TNew>(data, Pagination.PageNumber, Pagination.PageSize, Pagination.TotalRecords);
        return grid;
    }

    public DataListResult<T> ToDataListResult()
    {
        return this;
    }
}