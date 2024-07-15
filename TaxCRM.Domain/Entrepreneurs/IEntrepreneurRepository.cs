using TaxCRM.Domain.Common;

namespace TaxCRM.Domain.Entrepreneurs;

public interface IEntrepreneurRepository : IRepository<Entrepreneur>
{
    Task<ICollection<Entrepreneur>> GetAll();
}

public interface IEntrepreneurProfileRepository : IRepository<EntrepreneurProfile> 
{
    Task<ICollection<EntrepreneurProfile>> GetByEntrepreneur(Guid entrepreneurId);
    Task<bool> AnyByEntrepreneurAndCountry(Guid entrepreneurId, Country country);
}
