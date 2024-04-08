using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers;

public class ErrorsController : ApiController
{
    public const string Route = "/error";
    
    [AllowAnonymous]
    [Route(Route)]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        
        return Problem(title: exception?.Message);
    }
}