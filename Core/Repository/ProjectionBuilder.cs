using System.Linq.Expressions;

namespace Core.Repository;

public static class ProjectionBuilder
{
    public static string Field<T>(Expression<Func<T, object>> field)
    {
        return field.ToString().Replace("x => x.", "").Replace(".get_Item(-1)", "");
    }
}