using MOD.UserService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOD.UserService.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUserById(int id);
        IEnumerable<User> GetUserByName(string name);
        int SaveUser(User user);
        int UpdateUser(User user);
        bool DeleteUserById(int id);
        bool DeleteUserByName(string name);
    }
}
