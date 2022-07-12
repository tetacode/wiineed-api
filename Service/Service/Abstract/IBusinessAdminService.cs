using Core.Service.Result;
using Data.Entity.Collection;

namespace Service.Service.Abstract;

public interface IBusinessAdminService
{
    public DataResult<Business> GetBusiness(Guid key);
}