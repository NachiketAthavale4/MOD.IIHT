using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MOD.AuthenticationService.Contexts;
using MOD.AuthenticationService.Models;

namespace MOD.AuthenticationService.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly AuthenticationContext context;

        public AuthenticationRepository(AuthenticationContext context)
        {
            this.context = context;
        }

        public bool AuthenticateUser(User user)
        {
            try
            {
                var loggedInUser = context.Users.FirstOrDefault(
                    usr => usr.UserName.Equals(
                        user.UserName, StringComparison.OrdinalIgnoreCase));

                if (loggedInUser != null
                    && user.Password.Equals(
                        loggedInUser.Password, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
