namespace Core.Entity;

public interface IEntity
{
    
}

public interface IEntity<out TKey> : IEntity where TKey : IEquatable<TKey>
{
    public TKey Id { get; }
    public DateTime CreatedDate { get; set; }
    public bool IsDeleted { get; set; }
}