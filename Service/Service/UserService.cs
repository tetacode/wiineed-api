using Core.Repository;
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

    public DataResult<User> GetUser(string id)
    {
        return _userRepository
            .Query()
            .FirstOrDefault(x => x.Id == id).DataResult();
    }

    public DataGridResult<User> GetUserGrid(DataGridInput gridInput)
    {
        return _userRepository
            .Query()
            .ToDataGrid(gridInput.PaginationQuery)
            .DataGridResult();
    }
}