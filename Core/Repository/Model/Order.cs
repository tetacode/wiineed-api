using System.Linq.Expressions;

namespace Core.Repository.Model;

public class Order<T>
{
    public Expression<Func<T, object>> Exp { get; set; }
    public OrderOperation Operation { get; set; }
}