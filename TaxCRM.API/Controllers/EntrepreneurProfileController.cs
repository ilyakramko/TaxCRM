using Microsoft.AspNetCore.Mvc;
using TaxCRM.API.Infrastructure.Responses;
using TaxCRM.Application.Entrepreneurs;

namespace TaxCRM.API.Controllers;

[ApiController]
[Route("api/entrepreneurs/{entrepreneurId}/profiles")]
public class EntrepreneurProfileController(EntrepreneurService entrepreneurService) : ControllerBase
{ 
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute]Guid id)
    {
        var profile = await entrepreneurService.GetEntrepreneurProfile(id);
        return profile.ToObjectResult();
    }

    [HttpGet]
    public async Task<IActionResult> GetProfiles([FromRoute]Guid entrepreneurId)
    {
        var profiles = await entrepreneurService.GetEntrepreneurProfiles(entrepreneurId);
        return profiles.ToObjectResult();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromRoute]Guid entrepreneurId, [FromBody]EntrepreneurProfileView view)
    {
        var profile = await entrepreneurService.AddProfile(view, entrepreneurId);
        return profile.ToObjectResult();
    }
}
