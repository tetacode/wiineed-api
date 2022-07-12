using System.Linq.Expressions;

namespace Core.Repository;

public static class Fielder
{
    public static string ClassName(Type T)
    {
        return T.Name.ToLower();
    }
    
    public static string Field<T>(Expression<Func<T, object>> field)
    {
        return field.ToString().Replace("x => x.", "").Replace(".get_Item(-1)", "");
    }
}