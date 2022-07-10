using Core.Repository.Abstract;
using Data.Entity;

namespace Data.Repository.Abstract;

public interface IUserRepository : IRepository<User, string>
{
    
}