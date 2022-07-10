using Core.Repository;
using Data.Entity;
using Data.Repository.Abstract;
using Microsoft.Extensions.Options;

namespace Data.Repository;

public class PermissionRepository : MongoDbRepositoryBase<Permission>, IPermissionRepository
{
    public PermissionRepository(IOptions<MongoDbSettings> options) : base(options)
    {
    }
}