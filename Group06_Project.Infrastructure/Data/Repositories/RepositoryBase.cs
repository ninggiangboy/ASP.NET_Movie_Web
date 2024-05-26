using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Group06_Project.Domain.Interfaces.Repositories;
using Group06_Project.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Group06_Project.Infrastructure.Data.Repositories;

public class RepositoryBase<T, TId> : IRepositoryBase<T, TId> where T : class
{
    private readonly DbSet<T> _dbSet;

    protected RepositoryBase(ApplicationDbContext appDbContext)
    {
        _dbSet = appDbContext.Set<T>();
    }

    public void Add(T entity)
    {
        _dbSet.Add(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        _dbSet.AddRange(entities);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
    {
        return _dbSet.AddRangeAsync(entities, cancellationToken);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public IQueryable<T> GetAll()
    {
        return _dbSet;
    }

    public long Count()
    {
        return _dbSet.Count();
    }

    public Page<T> GetAll(PageRequest<T> pageRequest)
    {
        var skip = (pageRequest.PageNumber - 1) * pageRequest.Size;
        var data = _dbSet.Skip(skip).Take(pageRequest.Size).OrderBy(pageRequest.Sort ?? "Id desc");
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
        return _dbSet.Where(predicate);
    }

    public Page<T> GetByExpression(Expression<Func<T, bool>> predicate, PageRequest<T> pageRequest)
    {
        var skip = (pageRequest.PageNumber - 1) * pageRequest.Size;
        var data = _dbSet.Where(predicate).Skip(skip).Take(pageRequest.Size).OrderBy(pageRequest.Sort ?? "Id desc");
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
        return _dbSet.Find(id);
    }
}