using System.Collections.Generic;

namespace MOD.UserService.Models
{
    public class UserResponse
    {
        public IEnumerable<User> UserList { get; set; }
        public Fault Fault { get; set; }
        public bool SuccessIndicator { get; set; }
    }
}
