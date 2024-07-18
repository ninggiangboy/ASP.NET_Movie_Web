using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Interfaces.Repositories;
using Group06_Project.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

    public Page<UserList> GetUserList(PageRequest<User> pageRequest, Expression<Func<User, bool>> expression,
        string? search, int? page)
    {
        var skip = (pageRequest.PageNumber - 1) * pageRequest.Size;
        var rawData = DbSet
            .Include(u => u.Comments)
            .GroupJoin(
                DbContext.Set<IdentityUserRole<string>>(),
                user => user.Id,
                userRole => userRole.UserId,
                (user, userRoles) => new { User = user, UserRoles = userRoles })
            .SelectMany(
                x => x.UserRoles.DefaultIfEmpty(),
                (x, userRole) => new { x.User, RoleId = userRole != null ? userRole.RoleId : null })
            .GroupJoin(
                DbContext.Set<IdentityRole>(),
                x => x.RoleId,
                role => role.Id,
                (x, roles) => new { x.User, Roles = roles })
            .SelectMany(
                x => x.Roles.DefaultIfEmpty(),
                (x, role) => new { x.User, RoleName = role != null ? role.Name : null })
            .Where(x => x.RoleName != "Admin" || x.RoleName == null)
            .Where(expression);
        var totalElement = rawData.Count();
        var data = rawData
            //.OrderBy(pageRequest.Sort ?? "Id desc")
            .Skip(skip).Take(pageRequest.Size)
            .Select(u => new UserList
            {
                Id = u.User.Id,
                UserName = u.User.UserName,
                Email = u.User.Email,
                PhoneNumber = u.User.PhoneNumber,
                LockoutEnabled = u.User.LockoutEnabled,
                totalComment = u.User.Comments.Count
            });
        return new Page<UserList>
        {
            PageNumber = pageRequest.PageNumber,
            PageSize = pageRequest.Size,
            TotalElement = totalElement,
            Data = totalElement == 0 ? new List<UserList>() : data.ToList()
        };
    }

    //public User GetUserById(string userId)
    //{
    //    return DbSet.FirstOrDefault(u => u.Id == userId);
    //}
    public void DisableUserLockout(User user)
    {
        DbSet.Update(user);
        DbContext.SaveChanges();
    }

    public void EnableUserLockout(User user)
    {
        DbSet.Update(user);
        DbContext.SaveChanges();
    }
}