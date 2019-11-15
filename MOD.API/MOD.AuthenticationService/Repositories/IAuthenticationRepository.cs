using MOD.AuthenticationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOD.AuthenticationService.Repositories
{
    public interface IAuthenticationRepository
    {
        bool AuthenticateUser(User user);
    }
}
