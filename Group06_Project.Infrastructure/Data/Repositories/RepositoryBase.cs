using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Group06_Project.Domain.Interfaces.Repositories;
using Group06_Project.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Group06_Project.Infrastructure.Data.Repositories;

public class RepositoryBase<T, TId> : IRepositoryBase<T, TId> where T : class
{
    protected readonly DbSet<T> DbSet;

    protected RepositoryBase(ApplicationDbContext appDbContext)
    {
        DbSet = appDbContext.Set<T>();
    }

    public void Add(T entity)
    {
        DbSet.Add(entity);
    }

    public void AddAll(IEnumerable<T> entities)
    {
        DbSet.AddRange(entities);
    }

    public async Task AddAsync(T entity)
    {
        await DbSet.AddAsync(entity, CancellationToken.None);
    }

    public Task AddRangeAsync(IEnumerable<T> entities)
    {
        return DbSet.AddRangeAsync(entities, CancellationToken.None);
    }

    public void Remove(T entity)
    {
        DbSet.Remove(entity);
    }

    public IEnumerable<T> GetAll()
    {
        return DbSet.ToList();
    }

    public long Count()
    {
        return DbSet.Count();
    }

    public Page<T> GetAll(PageRequest<T> pageRequest)
    {
        var skip = (pageRequest.PageNumber - 1) * pageRequest.Size;
        var data = DbSet.Skip(skip).Take(pageRequest.Size).OrderBy(pageRequest.Sort ?? "Id desc");
        var totalElement = data.Count();
        return new Page<T>
        {
            PageNumber = pageRequest.PageNumber,
            PageSize = pageRequest.Size,
            TotalElement = totalElement,
            Data = totalElement == 0 ? new List<T>() : data.ToList()
        };
    }

    public IQueryable<T> GetByExpression(Expression<Func<T, bool>> predicate)
    {
        return DbSet.Where(predicate);
    }

    public Page<T> GetByExpression(Expression<Func<T, bool>> predicate, PageRequest<T> pageRequest)
    {
        var skip = (pageRequest.PageNumber - 1) * pageRequest.Size;
        var data = DbSet.Where(predicate).Skip(skip).Take(pageRequest.Size).OrderBy(pageRequest.Sort ?? "Id desc");
        var totalElement = data.Count();
        return new Page<T>
        {
            PageNumber = pageRequest.PageNumber,
            PageSize = pageRequest.Size,
            TotalElement = totalElement,
            Data = totalElement == 0 ? new List<T>() : data.ToList()
        };
    }

    public T? GetById(TId id)
    {
        return DbSet.Find(id);
    }

    public void Update(T entity)
    {
        DbSet.Update(entity);
    }
}