using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group06_Project.Domain.Interfaces.Repositories;

public interface IUserRepository : IRepositoryBase<User, string>
{
    User GetByIdWithFavoriteFilms(string userId);
    Page<UserList> GetUserList(PageRequest<User> pageRequest, string? search, int? page);
    void DisableUserLockout(User user);
    void EnableUserLockout(User user);
}
