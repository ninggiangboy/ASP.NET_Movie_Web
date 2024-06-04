using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group06_Project.Infrastructure.Data.Repositories;
public class UserRepository : RepositoryBase<User, string>, IUserRepository
{
    public UserRepository(ApplicationDbContext appDbContext) : base(appDbContext)
    {
    }

    public User GetByIdWithFavoriteFilms(string userId)
    {
        return DbSet.Include(u => u.FavoriteFilms).FirstOrDefault(u => u.Id == userId);
    }
}
