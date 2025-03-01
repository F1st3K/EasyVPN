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
    public async Task<IActionResult> CreatePage([FromBody] DynamicPage page)
    {
        return Ok();
    }
    
    [HttpGet("{pageRoute}")]
    public async Task<IActionResult> GetPage([FromRoute] string pageRoute)
    {
        return Ok();
    }
    
    [HttpPut("{pageRoute}")]
    public async Task<IActionResult> UpdatePage([FromBody] DynamicPage page)
    {
        return Ok();
    }
    
    [HttpDelete("{pageRoute}")]
    public async Task<IActionResult> DeletePage([FromRoute] string pageRoute)
    {
        return Ok();
    }
}