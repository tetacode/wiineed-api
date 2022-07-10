using System.Linq.Expressions;
using Core.Repository.Abstract;
using Core.Repository.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Core.Repository;

public abstract class RepositoryBase<T> : IRepository<T> where T : class, new()
{
    private DbContext Context { get; set; }
    protected DbSet<T> DbSet { get; set; }
    
    private IDbContextTransaction _transaction;

    private IQueryable<T> _query;

    private List<Expression<Func<T, bool>>>? Predicates { get; set; } = null;
    private List<OrderByItem> OrderByItems { get; set; } = new List<OrderByItem>();
    
    private int _pageSize = 10;
    private int _pageNumber = 1;
    
    protected RepositoryBase(DbContext dbContext)
    {
        Context = dbContext;
        DbSet = dbContext.Set<T>();
        _query = DbSet.AsQueryable();
    }
    
    public T Add(T data)
    {
        T newData = DbSet.Add(data).Entity;
        SaveChanges();
        return newData;
    }

    public T Update(T data)
    {
        T newData = DbSet.Update(data).Entity;
        SaveChanges();
        return newData;
    }

    public void UpdateRange(IEnumerable<T> data)
    {
        DbSet.UpdateRange(data);
        SaveChanges();
    }

    public void AddRange(IEnumerable<T> data)
    {
        DbSet.AddRange(data);
        SaveChanges();
    }

    public void Delete(T data)
    {
        DbSet.Remove(data);
        SaveChanges();
    }

    public void DeleteRange(IEnumerable<T> data)
    {
        DbSet.RemoveRange(data);
        SaveChanges();
    }

    public void SaveChanges()
    {
        Context.SaveChanges();
    }

    public IQueryable<T> Query()
    {
        return DbSet.AsQueryable();
    }

    public void BeginTransaction()
    {
        _transaction = Context.Database.BeginTransaction();
    }

    public void CommitTransaction()
    {
        _transaction.Commit();
    }

    public void RollbackTransaction()
    {
        _transaction.Rollback();
    }
}