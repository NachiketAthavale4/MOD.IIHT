using Microsoft.AspNetCore.Mvc;
using MOD.PaymentService.Models;

namespace MOD.PaymentService.Helpers
{
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(PaymentResponse faultResponse) 
            : base(faultResponse)
        {
            StatusCode = 500;
        }
    }
}
