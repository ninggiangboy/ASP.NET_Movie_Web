using System.Linq.Expressions;
using Group06_Project.Domain.Models;

namespace Group06_Project.Domain.Interfaces.Repositories;

public interface IRepositoryBase<T, in TId> where T : class
{
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    Task AddAsync(T entity, CancellationToken cancellationToken);
    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
    void Update(T entity);
    void Remove(T entity);
    IQueryable<T> GetAll();
    long Count();
    Page<T> GetAll(PageRequest<T> pageRequest);
    IQueryable<T> GetByExpression(Expression<Func<T, bool>> predicate);
    Page<T> GetByExpression(Expression<Func<T, bool>> predicate, PageRequest<T> pageRequest);
    T? GetById(TId id);
}