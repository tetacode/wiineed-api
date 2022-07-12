using System.Linq.Expressions;

namespace TestApp;

public static class ProjectionBuilder
{
    public static string Field<T>(Expression<Func<T, object>> field)
    {
        return field.ToString().Replace("x => x.", "").Replace(".get_Item(-1)", "");
    }
    
    public static string ClassName(Type x)
    {
        return x.Name;
    }
}

public class Business
{
    public List<Menu> Menus;
    public class Menu
    {
        public List<Category> Categories;
        public Category Cat;
        public class Category
        {
            public string Name;
        }
    }
}

public static class App
{
    public static void Main(string[] args)
    {
        var field = ProjectionBuilder.Field<Business>(x => x.Menus[-1].Categories[-1].Name);
        var name = ProjectionBuilder.ClassName(typeof(Business));
    }
}