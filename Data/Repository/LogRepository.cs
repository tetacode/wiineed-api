using Core.Repository;
using Data.Entity;
using Data.Repository.Abstract;

namespace Data.Repository;

public class LogRepository : RepositoryBase<Log>, ILogRepository
{
    public LogRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}