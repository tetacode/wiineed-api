using Core.Repository;
using Core.Service;
using Core.Service.Result;
using Data.Entity;
using Data.Entity.Collection;
using Data.Repository.Abstract;
using Data.StaticRepository;
using MongoDB.Driver;
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

    public DataResult<User> GetUser(Guid id)
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

    public void ChangeLanguage(LanguageCodeEnum languageCode)
    {
        var updateBuilder = Builders<User>.Update.Set(x => x.Settings.LanguageCode, languageCode);
        var res = _userRepository
            .Collection()
            .UpdateOne(x => x.Id == _user.Id, updateBuilder);
        var x = 10;
    }
}