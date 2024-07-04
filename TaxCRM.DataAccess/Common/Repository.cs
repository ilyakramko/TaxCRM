using TaxCRM.Domain.Common;

namespace TaxCRM.DataAccess.Common;

public abstract class Repository<T> : IRepository<T> where T : Entity
{
    protected readonly ApplicationDbContext dbContext;

    public Repository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<T> Add(T entity)
    {
        entity.Id = GenerateId();

        dbContext.Add(entity);
        await dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<T?> Get(Guid id)
    {
        return await dbContext.FindAsync<T>(id);
    }

    public async Task<T> Update(T entity)
    {
        dbContext.Update(entity);
        await dbContext.SaveChangesAsync();

        return entity;
    }

    protected Guid GenerateId()
    {
        //Generate Id on db side?
        return Guid.NewGuid();
    }
}
