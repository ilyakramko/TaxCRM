using Microsoft.EntityFrameworkCore;
using TaxCRM.DataAccess.Common;
using TaxCRM.Domain.Entrepreneurs;

namespace TaxCRM.DataAccess.Entrepreneurs;

public class EntrepreneurRepository : Repository<Entrepreneur>, IEntrepreneurRepository
{
    public EntrepreneurRepository(ApplicationDbContext dbContext) : base(dbContext) {}

    public async Task<ICollection<Entrepreneur>> GetAll() =>
        await dbContext.Entrepreneurs.ToListAsync();
}
