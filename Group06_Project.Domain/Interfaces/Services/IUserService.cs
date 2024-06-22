using Group06_Project.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group06_Project.Domain.Interfaces.Services;
public interface IUserService
{
    Page<UserList> GetUserList(string? search, int? pageNo);

    bool DisableUserLockout(string userId);

    bool EnableUserLockout(string userId);
}