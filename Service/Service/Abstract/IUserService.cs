using Core.Service.Result;
using Data.Entity;

namespace Service.Service.Abstract;

public interface IUserService
{
    public DataResult<User> GetUser(Guid uuid);
}