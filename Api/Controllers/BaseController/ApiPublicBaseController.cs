using System.Net;
using Core.Service.Result;
using Core.Service.Result.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.BaseController;

[ApiController]
[Route("api/[controller]")]
[ValidateApiModelState]
public abstract class ApiPublicControllerBase : ControllerBase
{
    public class ExceptionResult
    {
        public string Message { get; set; }
        public object ErrorData { get; set; }
        public Exception Exception { get; set; }
    }

    [ApiExplorerSettings(IgnoreApi=true)]
    public IActionResult CreateResult(IDataResult serviceQueryResult)
    {
        if (string.IsNullOrEmpty(serviceQueryResult.Message))
        {
            return StatusCode((int)HttpStatusCode.OK, serviceQueryResult);
        }

        return StatusCode((int)HttpStatusCode.PartialContent, serviceQueryResult);
    }
    
    [ApiExplorerSettings(IgnoreApi=true)]
    public IActionResult CreateResult()
    {
        return StatusCode((int)HttpStatusCode.OK, new DataResult<int>((int)HttpStatusCode.OK));
    }
}