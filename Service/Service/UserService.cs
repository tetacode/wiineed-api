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
        var findOptions = new FindOptions<User>();

        var s = _userRepository
            .Collection()
            .Find(x => x.Id == id)
            .Project(x => x.Settings)
            .FirstOrDefault();
        
        return _userRepository
            .Query()
            .FirstOrDefault().DataResult();
    }

    public void ChangeLanguage(Guid id, LanguageCodeEnum languageCode)
    {
        var updateBuilder = Builders<User>.Update.Set(x => x.Settings.LanguageCode, languageCode);
        var res = _userRepository
            .Collection()
            .UpdateOne(x => x.Id == id, updateBuilder);
    }
}