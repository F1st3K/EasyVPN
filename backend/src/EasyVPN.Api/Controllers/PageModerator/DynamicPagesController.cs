using EasyVPN.Api.Common;
using EasyVPN.Application.DynamicPages.Commands.CreateDynamicPage;
using EasyVPN.Application.DynamicPages.Commands.RemoveDynamicPage;
using EasyVPN.Application.DynamicPages.Commands.UpdateDynamicPage;
using EasyVPN.Application.DynamicPages.Queries.GetDynamicPage;
using EasyVPN.Application.DynamicPages.Queries.GetDynamicPages;
using EasyVPN.Contracts.DynamicPages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers.PageModerator;

[Route("dynamic-pages")]
public class DynamicPagesController : ApiControllerBase
{
    private readonly ISender _sender;

    public DynamicPagesController(ISender sender)
    {
        _sender = sender;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetPages()
    {
        var pagesResult = await _sender.Send(new GetDynamicPagesQuery());

        return pagesResult.Match(r => Ok(
            r.Select(p => new DynamicPageInfo(
                    p.Route,
                    p.Title,
                    p.LastModified,
                    p.Created
                ))), Problem);
    }

    [AllowAnonymous]
    [HttpGet("{*pageRoute}")]
    public async Task<IActionResult> GetPage([FromRoute] string pageRoute)
    {
        var getPageResult =
            await _sender.Send(new GetDynamicPageQuery(pageRoute));


        return getPageResult.Match(r => Ok(new DynamicPageResponse(
                r.Route,
                r.Title,
                r.LastModified,
                r.Created,
                r.Content ?? string.Empty
            )), Problem);
    }

    [Authorize(Roles = Roles.PageModerator)]
    [HttpPost]
    public async Task<IActionResult> CreatePage([FromBody] DynamicPageRequest pageRequest)
    {
        var result = await _sender.Send(new CreateDynamicPageCommand(
            pageRequest.Route,
            pageRequest.Title,
            pageRequest.Base64Content
        ));

        return result.Match(r => Created(), Problem);
    }

    [Authorize(Roles = Roles.PageModerator)]
    [HttpPut("{*pageRoute}")]
    public async Task<IActionResult> UpdatePage([FromRoute] string pageRoute, [FromBody] DynamicPageRequest pageRequest)
    {
        var result = await _sender.Send(new UpdateDynamicPageCommand(
            pageRoute,
            pageRequest.Route,
            pageRequest.Title,
            pageRequest.Base64Content
        ));

        return result.Match(r => Ok(), Problem);
    }

    [Authorize(Roles = Roles.PageModerator)]
    [HttpDelete("{*pageRoute}")]
    public async Task<IActionResult> DeletePage([FromRoute] string pageRoute)
    {
        var result = await _sender.Send(new RemoveDynamicPageCommand(pageRoute));

        return result.Match(r => Ok(), Problem);
    }
}