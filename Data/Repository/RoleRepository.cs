using Core.Repository;
using Data.Entity;
using Data.Repository.Abstract;

namespace Data.Repository;

public class RoleRepository : RepositoryBase<Role>, IRoleRepository
{
    public RoleRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}