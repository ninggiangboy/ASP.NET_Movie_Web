using System.Linq.Expressions;
using Group06_Project.Domain.Models;

namespace Group06_Project.Domain.Interfaces.Repositories;

public interface IRepositoryBase<T, in TId> where T : class
{
    void Add(T entity);
    void AddAll(IEnumerable<T> entities);
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    void Update(T entity);
    void Remove(T entity);
    IEnumerable<T> GetAll();
    long Count();
    Page<T> GetAll(PageRequest<T> pageRequest);
    IQueryable<T> GetByExpression(Expression<Func<T, bool>> predicate);
    Page<T> GetByExpression(Expression<Func<T, bool>> predicate, PageRequest<T> pageRequest);
    T? GetById(TId id);
}