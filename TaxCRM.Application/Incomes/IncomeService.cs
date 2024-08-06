using Mapster;
using TaxCRM.Domain.Incomes;
using TaxCRM.Domain.Results;
using TaxCRM.Domain.Results.Errors;
using TaxCRM.Utils.Guards;

namespace TaxCRM.Application.Incomes;

public class IncomeService(IIncomeRepository incomeRepository)
{
    public async Task<Result<IncomeView>> AddIncome(IncomeView view, Guid entrepreneurProfileId)
	{
        var newIncome = Income.Create(view.Amount, view.Currency, view.Date, entrepreneurProfileId);
        if (!newIncome.Success)
            return newIncome.Error ?? throw new ArgumentException("The error shouldn't be null");

        Guard.ArgumentIsNotNull(newIncome.Data, "The success result data shouldn't be null");

        var income = await incomeRepository.Add(newIncome.Data);

		return income.Adapt<IncomeView>();
    }

	public async Task<Result<IncomeView>> GetIncome(Guid id)
	{
		var income = await incomeRepository.Get(id);

        if (income is null)
            return Errors.Income.NotFound;

        return income.Adapt<IncomeView>();
    }

    //TODO: Return type is awful
    public async Task<Result<List<IncomeView>>> GetIncomes(Guid profileId)
	{
		var incomes = await incomeRepository.GetByProfile(profileId);
        return incomes.Adapt<List<IncomeView>>();
    }

    //TODO: Able to delete the income only if no info accosiated with it
}
