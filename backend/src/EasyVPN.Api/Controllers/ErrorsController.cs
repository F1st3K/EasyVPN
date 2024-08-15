using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers;

public class ErrorsController : ApiController
{
    public const string Route = "/error";

    /// <summary>
    /// Errors endpoint. (any where)
    /// </summary>
    /// <returns>Returns the error information.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// GET {{host}}/error
    /// </remarks>
    [AllowAnonymous]
    [HttpGet(Route)]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        return Problem(title: exception?.Message);
    }
}
