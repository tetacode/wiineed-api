namespace Core.Repository.Abstract;

public interface IEntitySoftDelete : IEntity
{
    public bool IsDeleted { get; set; }
    public DateTime DeletedDate { get; set; }
}