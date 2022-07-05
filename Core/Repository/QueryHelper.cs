using System.Linq.Expressions;
using Core.Repository.Model;

namespace Core.Repository;

public class QueryHelper
{
    private static Expression GetExpression(ParameterExpression param, string fieldName)
    {
        MemberExpression property = null;
        string[] fieldNames = fieldName.Split('.');
        foreach (string filed in fieldNames)
        {
            if (property == null)
            {
                property = Expression.Property(param, filed);
            }
            else
            {
                property = Expression.Property(property, filed);
            }
        }

        return Expression.Convert(property, typeof(object));
    }
    
    private static IOrderedQueryable<T> SortIQueryable<T>(IQueryable<T> data, OrderByItem item)
    {
        var param = Expression.Parameter(typeof(T), "i");
        var mySortExpression = Expression.Lambda<Func<T, object>>(GetExpression(param, item.Name), param);

        return (item.Operation == OrderOperation.ASC) ? data.OrderByDescending(mySortExpression)
            : data.OrderBy(mySortExpression);
    }
    
    private static IOrderedQueryable<T> ThenSortIQueryable<T>(IOrderedQueryable<T> data, OrderByItem item)
    {
        var param = Expression.Parameter(typeof(T), "i");
        var mySortExpression = Expression.Lambda<Func<T, object>>(GetExpression(param, item.Name), param);

        var q = (item.Operation == OrderOperation.ASC) ? data.ThenByDescending(mySortExpression)
            : data.ThenBy(mySortExpression);

        return q;
    }
    public static IQueryable<T> CreateOrderByQuery<T>(IQueryable<T> query, List<OrderByItem> orderByItems)
    {
        IOrderedQueryable<T> q = null;
        bool then = false;

        for (int i = 0; i < orderByItems.Count; i++)
        {
            var order = orderByItems[i];
            
            if(order.Operation == OrderOperation.NONE)
                continue;

            if (!then)
            {
                q = SortIQueryable(query, order);
                then = true;
            }
            else
            {
                q = ThenSortIQueryable(q, order);
            }
        }

        if (q == null)
            return query;

        return q;
    }
}