using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MOD.UserService.Contexts;
using MOD.UserService.Models;

namespace MOD.UserService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext userContext;

        public UserRepository(UserContext userContext)
        {
            this.userContext = userContext;
        }

        public bool DeleteUserById(int id)
        {
            try
            {
                var user = userContext.Users.Find(id);
                if (user != null)
                {
                    userContext.Users.Remove(user);
                    userContext.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteUserByName(string name)
        {
            try
            {
                var user = userContext.Users.FirstOrDefault(
                    usr => usr.UserName.ToUpper().Contains(name.ToUpper()));
                if (user != null)
                {
                    userContext.Users.Remove(user);
                    userContext.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User GetUserById(int id)
        {
            try
            {
                return userContext.Users.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<User> GetUserByName(string userName)
        {
            try
            {
                var usersReturned =
                    userContext.Users.Where(usr =>
                    usr.UserName.ToUpper().Contains(userName.ToUpper()));
                return usersReturned.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<User> GetUsers()
        {
            try
            {
                return userContext.Users.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int SaveUser(User user)
        {
            try
            {
                userContext.Users.Add(user);
                return userContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int UpdateUser(User user)
        {
            try
            {
                var storeTechnology = userContext.Users.Attach(user);
                storeTechnology.State =
                    Microsoft.EntityFrameworkCore.EntityState.Modified;
                return userContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
