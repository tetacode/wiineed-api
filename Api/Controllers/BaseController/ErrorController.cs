using System.Net;
using Core.Service.Exception;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.BaseController;

public class ErrorController : ControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/error")]
    public IActionResult HandleError()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        var expResult = new ApiPublicControllerBase.ExceptionResult()
        {
            Message = exception?.Message
        };
        
        if (exception is ServiceException serviceException)
        {
            expResult.Message = serviceException.GetServiceMessage();
            expResult.ErrorData = serviceException.GetErrorData();

            return StatusCode((int)HttpStatusCode.ServiceUnavailable, expResult);
        }
        else if (exception is ServiceNotAllowedException allowedException)
        {
            expResult.Message = allowedException.GetServiceMessage();
            expResult.ErrorData = allowedException.GetErrorData();
            
            return StatusCode((int)HttpStatusCode.MethodNotAllowed, expResult);
        }
        
        return StatusCode((int)HttpStatusCode.BadRequest, expResult);
    }
        
    
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/error-development")]
    public IActionResult HandleErrorDevelopment()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        var expResult = new ApiPublicControllerBase.ExceptionResult()
        {
            Message = exception?.Message,
            Exception = exception
        };
        
        if (exception is ServiceException serviceException)
        {
            expResult.Message = serviceException.GetServiceMessage();
            expResult.ErrorData = serviceException.GetErrorData();

            return StatusCode((int)HttpStatusCode.ServiceUnavailable, expResult);
        }
        else if (exception is ServiceNotAllowedException allowedException)
        {
            expResult.Message = allowedException.GetServiceMessage();
            expResult.ErrorData = allowedException.GetErrorData();
            
            return StatusCode((int)HttpStatusCode.MethodNotAllowed, expResult);
        }
        
        return StatusCode((int)HttpStatusCode.BadRequest, expResult);
    }
}