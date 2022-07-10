using Core.Repository;
using Data.Entity;
using Data.Repository.Abstract;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Data.Repository;

public class UserRepository : MongoDbRepositoryBase<User>, IUserRepository
{
    public UserRepository(IOptions<MongoDbSettings> options, ILogger<UserRepository> logger) : base(options, logger)
    {
    }
}