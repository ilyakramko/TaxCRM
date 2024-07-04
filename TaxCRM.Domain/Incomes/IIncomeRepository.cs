using TaxCRM.Domain.Common;

namespace TaxCRM.Domain.Incomes;

public interface IIncomeRepository : IRepository<Income>
{
    Task<ICollection<Income>> GetByProfile(Guid profileId);
}
