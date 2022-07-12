using Core.Repository.Abstract;
using Core.Repository.Model;
using Core.Service.Result;
using MongoDB.Driver;

namespace Core.Repository;

public static class RepositoryExtension
{

    public static IQueryable<T> FilterModel<T>(this IQueryable<T> query, FilterModel<T> filter)
    {
        return filter.Where(query);
    }
    
    public static DataGrid<T> ToDataGrid<T>(this IQueryable<T> query, PaginationQuery pagination)
    {
        var count = query.Count();

        int skip = (pagination.PageNumber - 1) * pagination.PageSize;
        query = query.Skip(skip).Take(pagination.PageSize);

        return new DataGrid<T>(count, pagination.PageSize, pagination.PageNumber, query);
    }

    public static DataGrid<TRes> Select<T, TRes>(this DataGrid<T> grid, Func<T, TRes> selector)
    {
        return new DataGrid<TRes>(grid.totalRecords, grid.pageSize, grid.pageNumber, grid.Data.Select(selector));
    }

    public static IQueryable<T> OrderByItems<T>(this IQueryable<T> query, List<OrderByItem> orders, List<Order<T>>? defaultOrders = null)
    {
        if (!orders.Any(x => x.Operation != OrderOperation.NONE) && defaultOrders != null && defaultOrders.Any())
        {
            IOrderedQueryable<T> orderedQuery = query.OrderBy(defaultOrders[0].Exp);
            
            if(defaultOrders[0].Operation == OrderOperation.DESC)
            {
                orderedQuery = query.OrderByDescending(defaultOrders[0].Exp);
            }
            
            for (int i = 1; i < defaultOrders.Count; i++)
            {
                var item = defaultOrders[i];
                if (item.Operation == OrderOperation.ASC)
                {
                    orderedQuery = orderedQuery.ThenBy(item.Exp);
                }
                else if(item.Operation == OrderOperation.DESC)
                {
                    orderedQuery = orderedQuery.ThenByDescending(item.Exp);
                }
            }

            query = orderedQuery;
        }
        else if (orders.Any())
        {
            query = QueryHelper.CreateOrderByQuery(query, orders);
        }

        return query;
    }

    public static IQueryable<T> OrderByItems<T>(this IQueryable<T> query, List<OrderByItem> orders,
        Order<T> defaultOrder)
    {
        return OrderByItems(query, orders, new List<Order<T>>() { defaultOrder });
    }
}