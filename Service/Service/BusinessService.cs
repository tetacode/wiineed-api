using Data.Entity.Collection;
using Data.Repository.Abstract;
using Service.Service.Abstract;

namespace Service.Service;

public class BusinessService : IBusinessService
{
    private readonly IBusinessRepository _businessRepository;
    private readonly User _user;

    public BusinessService(IBusinessRepository businessRepository, User user)
    {
        _businessRepository = businessRepository;
        _user = user;
    }
}