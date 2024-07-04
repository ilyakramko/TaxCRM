using Microsoft.AspNetCore.Mvc;
using TaxCRM.API.Infrastructure.Responses;
using TaxCRM.Application.Entrepreneurs;

namespace TaxCRM.API.Controllers;

[ApiController]
[Route("api/entrepreneurs")]
public class EntrepreneurController(EntrepreneurService entrepreneurService) : ControllerBase
{
	[HttpGet("{id}")]
	public async Task<IActionResult> Get([FromRoute]Guid id)
    {
		var entrepreneur = await entrepreneurService.Get(id);
        return entrepreneur.ToObjectResult();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var entrepreneurs = await entrepreneurService.GetEntrepreneurs();
        return entrepreneurs.ToObjectResult();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody]EntrepreneurView view)
    {
        var entrepreneur = await entrepreneurService.Create(view);
        return entrepreneur.ToObjectResult();
    }
}
