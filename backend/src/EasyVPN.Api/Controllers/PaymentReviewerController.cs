using EasyVPN.Api.Common;
using EasyVPN.Application.Vpn.Commands.ConfirmConnection;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers;

[Route("payment")]
[Authorize(Roles = Roles.PaymentReviewer)]
public class PaymentReviewerController : ApiController
{
    private readonly ISender _sender;
    
    public PaymentReviewerController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpPost("{connectionId:guid}/confirm")]
    public async Task<IActionResult> Confirm([FromRoute] Guid connectionId)
    {
        var confirmResult = await _sender.Send(
            new ConfirmConnectionCommand(connectionId, 0));
        
        return confirmResult.Match(
            _ => Ok(), 
            errors => Problem(errors));
    }
}