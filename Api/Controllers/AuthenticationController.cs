using Api.Controllers.BaseController;
using Core.Service.Result;
using Microsoft.AspNetCore.Mvc;
using Service.Model.DataInput;
using Service.Model.Output;
using Service.Service.Abstract;

namespace Api.Controllers;

public class AuthenticationController : ApiPublicControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    [Produces(typeof(DataResult<AuthView>))]
    public IActionResult Authenticate([FromBody] Authenticate authenticate)
    {
        return CreateResult(_authenticationService.Authenticate(authenticate));
    }
}