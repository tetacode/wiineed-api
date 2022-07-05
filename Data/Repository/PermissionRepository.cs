using Core.Repository;
using Data.Entity;
using Data.Repository.Abstract;

namespace Data.Repository;

public class PermissionRepository : RepositoryBase<Permission>, IPermissionRepository
{
    public PermissionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}