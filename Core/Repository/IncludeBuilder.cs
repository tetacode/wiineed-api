using System.Linq.Expressions;

namespace Core.Repository;

public class IncludeBuilder<T>
{
    private List<string> _names = new List<string>();
    private Dictionary<string, List<string>> _dictionary = new Dictionary<string, List<string>>();
    private string _id = Guid.NewGuid().ToString();

    private string[] Parse(string name)
    {
        var full = name.Split(".");
        var sub = full[Range.StartAt(1)];
        return sub;
    }
    public IncludeBuilder<T> Include(Expression<Func<T, object>> exp)
    {
        _dictionary.Add(_id, _names);
        _id = Guid.NewGuid().ToString();
        _names = new List<string>();
        
        var arr = Parse(exp.Body.ToString());
        _names.AddRange(arr);
        
        return this;
    }
    
    public IncludeBuilder<TNew> ThenInclude<TNew>(Expression<Func<TNew, object>> exp)
    {
        var arr = Parse(exp.Body.ToString());
        _names.AddRange(arr);
        var r = new IncludeBuilder<TNew>();
        r._names = _names;
        return r;
    }

    public string[] Build()
    {
        var arr = new List<string>();
        foreach (var var in _dictionary)
        {
            arr.Add(string.Join(".", var.Value));
        }

        if (_names.Any())
        {
            arr.Add(string.Join(".", _names));
        }

        return arr.Where(x => !string.IsNullOrEmpty(x)).ToArray();
    }
}