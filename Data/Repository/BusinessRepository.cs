using Data.Entity.Collection;
using Data.Repository.Abstract;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Data.Repository;

public class BusinessRepository : MongoDbRepositoryBase<Business>, IBusinessRepository
{
    public BusinessRepository(IOptions<MongoDbSettings> options, ILogger<BusinessRepository> logger) : base(options, logger)
    {
    }
}