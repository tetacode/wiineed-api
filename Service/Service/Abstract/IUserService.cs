using Core.Service;
using Core.Service.Result;
using Data.Entity;
using Data.Entity.Collection;
using Data.StaticRepository;

namespace Service.Service.Abstract;

public interface IUserService
{
    public DataResult<User> GetUser(Guid id);
    public DataGridResult<User> GetUserGrid(DataGridInput gridInput);
    public void ChangeLanguage(LanguageCodeEnum languageCode);
}