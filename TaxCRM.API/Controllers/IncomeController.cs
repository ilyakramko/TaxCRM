using Microsoft.AspNetCore.Mvc;
using TaxCRM.API.Infrastructure.Responses;
using TaxCRM.Application.Incomes;

namespace TaxCRM.API.Controllers;

[ApiController]
[Route("api/entrepreneurs/{entrepreneurId}/profiles/{profileId}/incomes")]
public class IncomeController(IncomeService incomeService) : ControllerBase
{
	[HttpGet("{id}")]
	public async Task<IActionResult> Get([FromRoute]Guid id)
    {
		var income = await incomeService.GetIncome(id);
        return income.ToObjectResult();
    }

    [HttpGet]
    public async Task<IActionResult> GetIncomes([FromRoute]Guid profileId)
	{
        var incomes = await incomeService.GetIncomes(profileId);
        return incomes.ToObjectResult();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromRoute]Guid profileId, [FromBody]IncomeView view)
    {
        var income = await incomeService.AddIncome(view, profileId);
        return income.ToObjectResult();
    }
}
