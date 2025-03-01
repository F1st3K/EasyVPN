using System.Text;
using EasyVPN.Application.DynamicPages.Queries.GetDynamicPage;
using EasyVPN.Contracts.DynamicPages;
using MediatR;
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

    [HttpGet]
    public async Task<IActionResult> GetPages()
    {
        return Ok();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePage([FromBody] DynamicPageResponse pageResponse)
    {
        return Ok();
    }
    
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