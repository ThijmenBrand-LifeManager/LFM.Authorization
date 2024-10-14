using Microsoft.EntityFrameworkCore;

namespace LFM.Authorization.Repository.Repository;

public class RepositoryBase
{
    protected readonly DatabaseContext Context;
    
    protected RepositoryBase(DatabaseContext context)
    {
        Context = context;
    }
}

public abstract class RepositoryBase<TModel>(DatabaseContext context) : RepositoryBase(context)
    where TModel : class
{
    public async Task<TModel> CreateAsync(TModel obj, CancellationToken cancellationToken = default)
    {
        Context.Add(obj);
        await Context.SaveChangesAsync(cancellationToken);

        return obj;
    }
    
    public async Task UpdateAsync(CancellationToken cancellationToken = default)
    {
        await Context.SaveChangesAsync(cancellationToken);
    }

    public void DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        Context.Set<TModel>().Remove(Context.Set<TModel>().Find(id)!);
    }
    
    public virtual async Task<TModel?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await Context.Set<TModel>().FindAsync([id], cancellationToken: cancellationToken);
    }

    public virtual async Task<IEnumerable<TModel>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await Context.Set<TModel>().ToListAsync(cancellationToken);
    }
}