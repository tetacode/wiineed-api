using Core.Repository;
using Data.Entity;
using Data.Repository.Abstract;
using Microsoft.Extensions.Options;

namespace Data.Repository;

public class LogRepository : MongoDbRepositoryBase<Log>, ILogRepository
{
    public LogRepository(IOptions<MongoDbSettings> options) : base(options)
    {
    }
}