using Core.Repository.Model;
using Core.Service.Result;
using Core.Service.Result.Abstract;

namespace Core.Service;

public static class ResultExtension
{
    public static DataResult<T> DataResult<T>(this T? data)
    {
        return new DataResult<T>(data);
    }

    public static DataListResult<T> DataListResult<T>(this IEnumerable<T> data)
    {
        return new DataListResult<T>(data);
    }

    public static DataGridResult<T> DataGridResult<T>(this DataGrid<T> dataGrid)
    {
        return new DataGridResult<T>(dataGrid.Data, dataGrid.pageNumber, dataGrid.pageSize, dataGrid.totalRecords);
    }
}