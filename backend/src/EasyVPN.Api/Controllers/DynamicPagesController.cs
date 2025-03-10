using System.Text;
using EasyVPN.Application.DynamicPages.Queries.GetDynamicPage;
using EasyVPN.Application.DynamicPages.Queries.GetDynamicPages;
using EasyVPN.Contracts.DynamicPages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers;

[Route("dynamic-pages")]
public class DynamicPagesController : ApiController
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
    
    [HttpPost]
    public async Task<IActionResult> CreatePage([FromBody] DynamicPageResponse pageResponse)
    {
        return Ok();
    }
    
    [AllowAnonymous]
    [HttpGet("{pageRoute}")]
    public async Task<IActionResult> GetPage([FromRoute] string pageRoute)
    {
        var getPageResult =
            await _sender.Send(new GetDynamicPageQuery(pageRoute));

        
        return getPageResult.Match(r => Ok(new DynamicPageResponse(
                r.Route,
                r.Title,
                r.LastModified,
                r.Created,
                Convert.ToBase64String(Encoding.UTF8.GetBytes(r.Content ?? string.Empty))
            )), Problem);
    }
    
    [HttpPut("{pageRoute}")]
    public async Task<IActionResult> UpdatePage([FromBody] DynamicPageResponse pageResponse)
    {
        return Ok();
    }
    
    [HttpDelete("{pageRoute}")]
    public async Task<IActionResult> DeletePage([FromRoute] string pageRoute)
    {
        return Ok();
    }
}