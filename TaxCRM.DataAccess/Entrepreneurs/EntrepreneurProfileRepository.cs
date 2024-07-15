using Microsoft.EntityFrameworkCore;
using TaxCRM.DataAccess.Common;
using TaxCRM.Domain.Common;
using TaxCRM.Domain.Entrepreneurs;

namespace TaxCRM.DataAccess.Entrepreneurs;

public class EntrepreneurProfileRepository : Repository<EntrepreneurProfile>, IEntrepreneurProfileRepository
{
    public EntrepreneurProfileRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<ICollection<EntrepreneurProfile>> GetByEntrepreneur(Guid entrepreneurId) =>
        await dbContext.EntrepreneurProfiles.Where(x => x.EntrepreneurId == entrepreneurId).ToListAsync();

    public async Task<bool> AnyByEntrepreneurAndCountry(Guid entrepreneurId, Country country) =>
        await dbContext.EntrepreneurProfiles.Where(x => x.EntrepreneurId == entrepreneurId && x.Country == country).AnyAsync();
}
