using Microsoft.AspNetCore.Mvc;
using MOD.TrainingService.Models;

namespace MOD.TrainingService.Helpers
{
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(TrainingResponse faultResponse) 
            : base(faultResponse)
        {
            StatusCode = 500;
        }
    }
}
