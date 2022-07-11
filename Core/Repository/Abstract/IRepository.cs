using System.Linq.Expressions;
using Core.Entity;
using Core.Repository.Model;
using MongoDB.Driver;

namespace Core.Repository.Abstract;

public interface IRepository<T, in TKey> where T : class, IEntity<TKey>, new() where TKey : IEquatable<TKey>
{
    T Add(T data);

    void AddRange(IEnumerable<T> data);

    T Update(T data);

    void UpdateRange(IEnumerable<T> data);

    void Delete(T data);

    public void DeleteRange(IEnumerable<T> data);
    
    public IQueryable<T> Query();

    public IMongoCollection<T> Collection();

    void BeginTransaction();
        
    void CommitTransaction();
    
    void RollbackTransaction();
    
    
}