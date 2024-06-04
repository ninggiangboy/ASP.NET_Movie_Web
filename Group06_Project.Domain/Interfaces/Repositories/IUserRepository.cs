using Group06_Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group06_Project.Domain.Interfaces.Repositories;

public interface IUserRepository : IRepositoryBase<User, string>
{
    User GetByIdWithFavoriteFilms(string userId);
}
