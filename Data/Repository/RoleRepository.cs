using Core.Repository;
using Data.Entity;
using Data.Repository.Abstract;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Data.Repository;

public class RoleRepository : MongoDbRepositoryBase<Role>, IRoleRepository
{
    public RoleRepository(IOptions<MongoDbSettings> options, ILogger<RoleRepository> logger) : base(options, logger)
    {
    }
}