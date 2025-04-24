using EasyVPN.Application.DynamicPages.Commands.CreateDynamicPage;
using EasyVPN.Application.DynamicPages.Commands.RemoveDynamicPage;
using EasyVPN.Application.DynamicPages.Commands.UpdateDynamicPage;
using EasyVPN.Application.DynamicPages.Queries.GetDynamicPage;
using EasyVPN.Application.DynamicPages.Queries.GetDynamicPages;
using EasyVPN.Contracts.DynamicPages;
using EasyVPN.Domain.Common.Enums;
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

    /// <summary>
    /// Get pages information. (anywhere)
    /// </summary>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// GET {{host}}/dynamic-pages
    /// </remarks>
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

    /// <summary>
    /// Get page. (anywhere)
    /// </summary>
    /// <param name="pageRoute">Route of page.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// GET {{host}}/dynamic-pages/{{pageRoute}}
    /// </remarks>
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

    /// <summary>
    /// Create page. (page moderator)
    /// </summary>
    /// <param name="pageRequest">Page information.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// POST {{host}}/dynamic-pages/{{pageRoute}}
    /// </remarks>
    [Authorize(Roles = nameof(RoleType.PageModerator))]
    [HttpPost]
    public async Task<IActionResult> CreatePage([FromBody] DynamicPageRequest pageRequest)
    {
        var result = await _sender.Send(new CreateDynamicPageCommand(
            pageRequest.Route,
            pageRequest.Title,
            pageRequest.Base64Content
        ));

        return result.Match(_ => Created(), Problem);
    }

    /// <summary>
    /// Update page information. (page moderator)
    /// </summary>
    /// <param name="pageRoute">Route of page.</param>
    /// <param name="pageRequest">New page information.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// PUT {{host}}/dynamic-pages/{{pageRoute}}
    /// </remarks>
    [Authorize(Roles = nameof(RoleType.PageModerator))]
    [HttpPut("{*pageRoute}")]
    public async Task<IActionResult> UpdatePage([FromRoute] string pageRoute, [FromBody] DynamicPageRequest pageRequest)
    {
        var result = await _sender.Send(new UpdateDynamicPageCommand(
            pageRoute,
            pageRequest.Route,
            pageRequest.Title,
            pageRequest.Base64Content
        ));

        return result.Match(_ => Ok(), Problem);
    }

    /// <summary>
    /// Remove page. (page moderator)
    /// </summary>
    /// <param name="pageRoute">Route of page.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// DELETE {{host}}/dynamic-pages/{{pageRoute}}
    /// </remarks>
    [Authorize(Roles = nameof(RoleType.PageModerator))]
    [HttpDelete("{*pageRoute}")]
    public async Task<IActionResult> DeletePage([FromRoute] string pageRoute)
    {
        var result = await _sender.Send(new RemoveDynamicPageCommand(pageRoute));

        return result.Match(_ => Ok(), Problem);
    }
}