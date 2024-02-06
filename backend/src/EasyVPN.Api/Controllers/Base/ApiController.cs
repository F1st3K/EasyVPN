using EasyVPN.Api.Common.Http;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers.Base;

[ApiController]
[AllowAnonymous]
public abstract class ApiController : ControllerBase
{
    [Authorize]
    public class Auth : ApiController
    { }
    
    [Authorize(Roles = "User")]
    public new class User : ApiController
    { }
    
    [Authorize(Roles = "PaymentReviewer")]
    public class Reviewer : ApiController
    { }
    
    [Authorize(Roles = "Administrator")]
    public class Admin : ApiController
    { }
    
    protected IActionResult Problem(List<Error> errors)
    {
        HttpContext.Items[HttpContextItemKeys.Errors] = errors;
        
        var firstError = errors[0];

        var statusCode = firstError.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: firstError.Description);
    }
}