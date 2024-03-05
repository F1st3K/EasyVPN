using EasyVPN.Api.Common;
using EasyVPN.Application.Vpn.Commands.AddLifetimeConnection;
using EasyVPN.Application.Vpn.Commands.ResetLifetimeConnection;
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
    
    [HttpPut("{connectionId:guid}/confirm")]
    public async Task<IActionResult> Confirm([FromRoute] Guid connectionId)
    {
        var confirmResult = await _sender.Send(
            new AddLifetimeConnectionCommand(connectionId, 0));
        
        return confirmResult.Match(
            _ => Ok(), 
            errors => Problem(errors));
    }
    
    [HttpPut("{connectionId:guid}/reset")]
    public async Task<IActionResult> Reset([FromRoute] Guid connectionId)
    {
        var confirmResult = await _sender.Send(
            new ResetLifetimeConnectionCommand(connectionId));
        
        return confirmResult.Match(
            _ => Ok(), 
            errors => Problem(errors));
    }
}