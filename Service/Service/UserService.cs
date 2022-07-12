using Core.Repository;
using Core.Service;
using Core.Service.Result;
using Data.Entity;
using Data.Entity.Collection;
using Data.Repository.Abstract;
using Data.StaticRepository;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
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

    public DataResult<User> GetUser(Guid key)
    {
        var s = _userRepository
            .Collection()
            .Aggregate()
            .Match(x => x.Key == key)
            .Lookup(
                Fielder.ClassName(typeof(Business)),
                Fielder.Field<User>(x => x.BusinessId),
                Fielder.Field<Business>(x => x.Key),
                Fielder.Field<User>(x => x.Business)
            )
            .Unwind(Fielder.Field<User>(x => x.Business))
            .Project(new BsonDocument
            {
                {Fielder.Field<User>(x => x.Business.Menus), 1},
                {Fielder.Field<User>(x => x.Business.Name), 1},
                {Fielder.Field<User>(x => x.Business.Key), 1}
            })
            .FirstOrDefault();

        var user = BsonSerializer.Deserialize<User>(s);

        return _userRepository
            .Query()
            .FirstOrDefault().DataResult();
    }

    public void ChangeLanguage(Guid key, LanguageCodeEnum languageCode)
    {
        var updateBuilder = Builders<User>.Update.Set(x => x.Settings.LanguageCode, languageCode);
        var res = _userRepository
            .Collection()
            .UpdateOne(x => x.Key == key, updateBuilder);
    }
}