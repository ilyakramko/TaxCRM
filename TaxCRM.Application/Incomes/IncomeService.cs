using Mapster;
using TaxCRM.Domain.Common;
using TaxCRM.Domain.Incomes;
using TaxCRM.Domain.Results;
using TaxCRM.Domain.Results.Errors;
using TaxCRM.Utils;

namespace TaxCRM.Application.Incomes;

public class IncomeService(IIncomeRepository incomeRepository)
{
    public async Task<Result<IncomeView>> AddIncome(IncomeView view, Guid entrepreneurProfileId)
	{
		//Use DateTime Provider		
		var now = DateTime.UtcNow;
		var utcDate = view.Date.ToUniversalTime();

		if (view.Amount <= 0)
			return Result<IncomeView>.FromFailure(Errors.Income.AmountAboveZero);
        
        if (!EnumEx.TryValidate<Currency>(view.Currency, out var currency))
            return Result<IncomeView>.FromFailure(Errors.Income.CurrencyIdNotValid);

		if (utcDate > now)
			return Result<IncomeView>.FromFailure(Errors.Income.IncomeDate);

        var income = new Income()
		{
			Value = new Money(view.Amount, currency),
			Date = utcDate,
			EntrepreneurProfileId = entrepreneurProfileId
		};

		income = await incomeRepository.Add(income);

		return Result<IncomeView>.FromSuccess(income.Adapt<IncomeView>());
    }

	public async Task<Result<IncomeView>> GetIncome(Guid id)
	{
		var income = await incomeRepository.Get(id);

        if (income is null)
            return Result<IncomeView>.FromFailure(Errors.Income.NotFound);

        return Result<IncomeView>.FromSuccess(income.Adapt<IncomeView>());
    }

    public async Task<Result<ICollection<IncomeView>>> GetIncomes(Guid profileId)
	{
		var incomes = await incomeRepository.GetByProfile(profileId);
        return Result<ICollection<IncomeView>>.FromSuccess(incomes.Adapt<ICollection<IncomeView>>());
    }
}
