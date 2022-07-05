using Core.Repository;
using Data.Entity;
using Data.Repository.Abstract;

namespace Data.Repository;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}