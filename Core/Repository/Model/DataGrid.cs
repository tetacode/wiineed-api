namespace Core.Repository.Model;

public class DataGrid<T>
{
    public int totalRecords { get; set; } = 0;
    public int pageSize { get; set; } = 10;
    public int pageNumber { get; set; } = 1;
    public IEnumerable<T> Data { get; set; }

    public DataGrid(int totalRecords, int pageSize, int pageNumber, IEnumerable<T> data)
    {
        this.totalRecords = totalRecords;
        this.pageSize = pageSize;
        this.pageNumber = pageNumber;
        Data = data;
    }
}