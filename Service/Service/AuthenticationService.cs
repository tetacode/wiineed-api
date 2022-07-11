using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Service;
using Core.Service.Exception;
using Core.Service.Result;
using Data.Entity;
using Data.Entity.Collection;
using Data.Repository.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.Model.DataInput;
using Service.Model.Output;
using Service.Service.Abstract;
using Utilities.Password;

namespace Service.Service;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly ServiceConfiguration.Jwt _jwt;

    public AuthenticationService(IUserRepository userRepository, IOptions<ServiceConfiguration.Jwt> jwt)
    {
        _userRepository = userRepository;
        _jwt = jwt.Value;
    }

    public DataResult<AuthView> Authenticate(Authenticate authenticate)
    {
        var user = _userRepository
            .Query()
            .FirstOrDefault(x => x.Username == authenticate.Username);

        if (user == null)
        {
            throw new ServiceNotAllowedException("User not found!");
        }

        if (!PasswordHelper.VerifyPassword(authenticate.Password, Convert.FromBase64String(user.PasswordSalt),
                user.PasswordHash))
        {
            throw new ServiceNotAllowedException("Wrong Password!");
        }
        
        return new AuthView()
        {
            Token = GenerateJWT(user),
            Username = user.Username,
            Email = user.Email,
            Name = user.Name,
            Lastname = user.Lastname
        }.DataResult();
    }
    
    private string GenerateJWT(User user)
    {    
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));    
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);    
    
        var token = new JwtSecurityToken(_jwt.Issuer,    
            _jwt.Issuer,    
            new List<Claim>()
            {
                new Claim("Id", user.Id.ToString())
            },    
            expires: DateTime.Now.AddMinutes(120),    
            signingCredentials: credentials);    
    
        return new JwtSecurityTokenHandler().WriteToken(token);    
    }    
}