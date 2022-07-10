using Core.Service;
using Core.Service.Result;
using Data.Entity;

namespace Service.Service.Abstract;

public interface IUserService
{
    public DataResult<User> GetUser(string id);
    public DataGridResult<User> GetUserGrid(DataGridInput gridInput);
}