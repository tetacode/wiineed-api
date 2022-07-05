using Core.Service;
using Core.Service.Result;
using Data.Entity;
using Data.Repository.Abstract;
using Service.Service.Abstract;

namespace Service.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly User _user;

    public UserService(IUserRepository userRepository, User user)
    {
        _userRepository = userRepository;
        _user = user;
    }

    public DataResult<User> GetUser(Guid uuid)
    {
        return _userRepository
            .Where(x => x.Id == uuid)
            .Get().DataResult();
    }
}