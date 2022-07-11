using Data.Entity;
using Data.Entity.Collection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.BaseController;

[Authorize]   
public abstract class BaseApiController : ApiPublicControllerBase
{

    [ApiExplorerSettings(IgnoreApi=true)]
    public User CurrentUser()
    {
        return (User)HttpContext.Items["User"];
    }
}