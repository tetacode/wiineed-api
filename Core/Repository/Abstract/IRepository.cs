using System.Linq.Expressions;
using Core.Repository.Model;

namespace Core.Repository.Abstract;

public interface IRepository<T> where T : class, new()
{
    T Add(T data);

    void AddRange(IEnumerable<T> data);

    T Update(T data);
    
    
    void UpdateRange(IEnumerable<T> data);

    void Delete(T data);

    public void DeleteRange(IEnumerable<T> data);

    public T? Get();

    public IRepository<T> Filter(FilterModel<T> filter);

    public IRepository<T> Where(Expression<Func<T, bool>> predicate);

    public IRepository<T> Pagination(PaginationQuery? paginationQuery);

    public IRepository<T> Sort(List<OrderByItem> items);
    
    public IRepository<T> Sort(Expression<Func<T, object>> exp, OrderOperation op);

    public IRepository<T> Include(Expression<Func<T, object>> exp);
    public IRepository<T> Include(IncludeBuilder<T> builder);

    public void SaveChanges();
    
    public IEnumerable<T> GetList();

    public DataGrid<T> GetDataGrid();

    void BeginTransaction();
        
    void CommitTransaction();
    
    void RollbackTransaction();
    
    
}