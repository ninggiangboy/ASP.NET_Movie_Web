using System.Linq.Expressions;
using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Models;

namespace Group06_Project.Domain.Interfaces.Repositories;

public interface IUserRepository : IRepositoryBase<User, string>
{
    User GetByIdWithFavoriteFilms(string userId);

    Page<UserList> GetUserList(PageRequest<User> pageRequest, Expression<Func<User, bool>>? expression, string? search,
        int? page);

    void DisableUserLockout(User user);
    void EnableUserLockout(User user);
}