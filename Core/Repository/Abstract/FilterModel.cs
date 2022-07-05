using System.Linq.Expressions;

namespace Core.Repository.Abstract;

public abstract class FilterModel<T>
{
    public FilterModel()
    {
        
    }

    public abstract IQueryable<T> Where(IQueryable<T> query);
}