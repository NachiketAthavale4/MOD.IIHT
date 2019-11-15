using Microsoft.AspNetCore.Mvc;
using MOD.AuthenticationService.Models;

namespace MOD.AuthenticationService.Helpers
{
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(
            AuthenticationResponse faultResponse) 
            : base(faultResponse)
        {
            StatusCode = 500;
        }
    }
}
