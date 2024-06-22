using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.Interfaces;
using Group06_Project.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Group06_Project.Domain.Interfaces.Repositories;
using Group06_Project.Domain.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Group06_Project.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
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

        return _userRepository.GetUserList(pageRequest, search, pageNo);
    }

    public bool DisableUserLockout(string userId)
    {
        var user = _userRepository.GetById(userId);
        if (user == null)
        {
            return false;
        }

        user.LockoutEnabled = false;
        _userRepository.DisableUserLockout(user);
        return true;
    }

    public bool EnableUserLockout(string userId)
    {
        var user = _userRepository.GetById(userId);
        if (user == null)
        {
            return false;
        }

        user.LockoutEnabled = true;
        _userRepository.EnableUserLockout(user);
        return true;
    }
}
