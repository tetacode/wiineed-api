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
    private List<Order<T>> _expressions = new List<Order<T>>();
    private List<string> _includes = new List<string>();
    
    private int _pageSize = 10;
    private int _pageNumber = 1;
    private int take = 0;
    
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

    public T? Get()
    {
        var query = BuildQuery();
        return query.FirstOrDefault();
    }

    public TIn? Get<TIn>(IQueryable<TIn> query)
    {
        return query.FirstOrDefault();
    }

    public IRepository<T> Filter(FilterModel<T> filter)
    {
        if (filter == null)
            return this;

        _query = filter.Where(_query);
        
        return this;
    }

    public IRepository<T> Where(Expression<Func<T, bool>> predicate)
    {
        _query = _query.Where(predicate);
        return this;
    }

    public IRepository<T> Pagination(PaginationQuery? paginationQuery)
    {
        if (paginationQuery == null)
            return this;
        
        _pageNumber = paginationQuery.PageNumber;
        _pageSize = paginationQuery.PageSize;

        return this;
    }

    public IRepository<T> Sort(List<OrderByItem> items)
    {
        if (items == null)
            return this;
        
        OrderByItems = items;
        return this;
    }

    public IRepository<T> Sort(Expression<Func<T, object>> exp, OrderOperation op)
    {
        _expressions.Add(new Order<T>()
        {
            Exp = exp,
            Operation = op
        });
        return this;
    }

    public IRepository<T> Include(Expression<Func<T, object>> exp)
    {
        var full = exp.Body.ToString().Split(".");
        var sub = full[Range.StartAt(1)];
        var name = string.Join(".", sub);
        _includes.Add(name);
        return this;
    }

    public IRepository<T> Include(IncludeBuilder<T> builder)
    {
        _includes.AddRange(builder.Build());
        return this;
    }

    public void Middleware()
    {
        
    }

    public void SaveChanges()
    {
        Context.SaveChanges();
    }

    protected IQueryable<T> BuildQuery()
    {
        var tempQuery = _query;

        if (_expressions.Count > 0)
        {
            IOrderedQueryable<T> orderedQuery = tempQuery.OrderBy(_expressions[0].Exp);
            
            if(_expressions[0].Operation == OrderOperation.DESC)
            {
                orderedQuery = tempQuery.OrderByDescending(_expressions[0].Exp);
            }
            
            for (int i = 1; i < _expressions.Count; i++)
            {
                var item = _expressions[i];
                if (item.Operation == OrderOperation.ASC)
                {
                    orderedQuery = orderedQuery.ThenBy(item.Exp);
                }
                else if(item.Operation == OrderOperation.DESC)
                {
                    orderedQuery = orderedQuery.ThenByDescending(item.Exp);
                }
            }

            _expressions.Clear();
            tempQuery = orderedQuery;
        }

        if (OrderByItems.Any())
        {
            tempQuery = QueryHelper.CreateOrderByQuery(tempQuery, OrderByItems);
            OrderByItems.Clear();
        }

        foreach(var include in _includes)
        {
            tempQuery = tempQuery.Include(include);
        }
        _includes.Clear();

        _query = DbSet.AsQueryable();
        return tempQuery;
    }

    public IEnumerable<T> GetList()
    {
        return BuildQuery();
    }

    public IEnumerable<TIn> GetList<TIn>(IQueryable<TIn> query)
    {
        return query;
    }

    public DataGrid<T> GetDataGrid()
    {
        var query = BuildQuery();
        var count = query.Count();

        int skip = (_pageNumber - 1) * _pageSize;
        query = query.Skip(skip).Take(_pageSize);
        
        return new DataGrid<T>(count, _pageSize, _pageNumber, query);
    }

    public DataGrid<TIn> GetDataGrid<TIn>(IQueryable<TIn> query)
    {
        var count = query.Count();

        int skip = (_pageNumber - 1) * _pageSize;
        query = query.Skip(skip).Take(_pageSize);
        
        return new DataGrid<TIn>(count, _pageSize, _pageNumber, query);
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