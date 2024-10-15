using System.Linq.Expressions;
using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Interfaces;
using Group06_Project.Domain.Interfaces.Repositories;
using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.Models;

namespace Group06_Project.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Page<UserList> GetUserList(string? search, int? pageNo)
    {
        var pageRequest = new PageRequest<User>
        {
            PageNumber = pageNo ?? 1,
            Size = 10,
            Sort = ""
        };

        Expression<Func<User, bool>> predicate = u =>
            string.IsNullOrEmpty(search)
            || u.UserName.Contains(search)
            || u.Email.Contains(search);
        // x => x.RoleName != "Admin" || x.RoleName == null
        return _userRepository.GetUserList(pageRequest, predicate, search, pageNo);
    }

    public bool DisableUserLockout(string userId)
    {
        var user = _userRepository.GetById(userId);
        if (user == null) return false;

        user.LockoutEnabled = false;
        _userRepository.DisableUserLockout(user);
        return true;
    }

    public bool EnableUserLockout(string userId)
    {
        var user = _userRepository.GetById(userId);
        if (user == null) return false;

        user.LockoutEnabled = true;
        _userRepository.EnableUserLockout(user);
        return true;
    }
}