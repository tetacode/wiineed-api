using Core.Service.Result;
using Data.Entity;
using Service.Model.DataInput;
using Service.Model.Output;

namespace Service.Service.Abstract;

public interface IAuthenticationService
{
    public DataResult<AuthView> Authenticate(Authenticate authenticate);
}