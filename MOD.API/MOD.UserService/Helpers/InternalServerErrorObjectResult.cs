using Microsoft.AspNetCore.Mvc;
using MOD.UserService.Models;

namespace MOD.UserService.Helpers
{
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(UserResponse faultResponse) 
            : base(faultResponse)
        {
            StatusCode = 500;
        }
    }
}
