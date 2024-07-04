using Microsoft.EntityFrameworkCore;
using TaxCRM.DataAccess.Common;
using TaxCRM.Domain.Incomes;

namespace TaxCRM.DataAccess.Incomes;

public class IncomeRepository : Repository<Income>, IIncomeRepository
{
    public IncomeRepository(ApplicationDbContext dbContext) : base(dbContext) {}

    public async Task<ICollection<Income>> GetByProfile(Guid profileId) =>
        await dbContext.Incomes.Where(x => x.EntrepreneurProfileId == profileId).ToListAsync();
}
