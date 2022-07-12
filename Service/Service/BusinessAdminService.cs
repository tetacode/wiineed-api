using Core.Service;
using Core.Service.Result;
using Data.Entity.Collection;
using Data.Repository.Abstract;
using Service.Service.Abstract;

namespace Service.Service;

public class BusinessAdminService : IBusinessAdminService
{
    private readonly IBusinessRepository _businessRepository;

    public BusinessAdminService(IBusinessRepository businessRepository)
    {
        _businessRepository = businessRepository;
    }

    public DataResult<Business> GetBusiness(Guid key)
    {
        return _businessRepository
            .Query()
            .FirstOrDefault(x => x.Key == key)
            .DataResult();
    }
}