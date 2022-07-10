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
    
    public IQueryable<T> Query();

    public void SaveChanges();

    void BeginTransaction();
        
    void CommitTransaction();
    
    void RollbackTransaction();
    
    
}