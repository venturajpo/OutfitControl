using Microsoft.EntityFrameworkCore;

namespace OutfitControl.Database.Repositories;

public abstract class RepositoryBase<T>(ApplicationDbContext context)
    where T : class
{
    protected readonly ApplicationDbContext Context = context;
    protected readonly DbSet<T> DbSet = context.Set<T>();

    public virtual T? GetById(int id)
    {
        return DbSet.Find(id);
    }

    public virtual IQueryable<T> GetAll()
    {
        return DbSet.AsQueryable();
    }

    public virtual T Add(T entity)
    {
        DbSet.Add(entity);
        Context.SaveChanges();
        return entity;
    }

    public virtual T Update(T entity)
    {
        DbSet.Update(entity);
        Context.SaveChanges();
        return entity;
    }

    public virtual T Remove(T entity)
    {
        DbSet.Remove(entity);
        Context.SaveChanges();
        return entity;
    }
    
}