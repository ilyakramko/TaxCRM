namespace TaxCRM.Domain.Common;

public interface IRepository<T> where T: Entity
{
    public Task<T> Add(T entity);
    public Task<T?> Get(Guid id);
    public Task<T> Update(T entity);
    //public Task<int> Delete(Guid id);
}
