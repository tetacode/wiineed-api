using Core.Repository.Abstract;
using Data.Entity;
using Data.Entity.Collection;

namespace Data.Repository.Abstract;

public interface IUserRepository : IRepository<User, Guid>
{
    
}