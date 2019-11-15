using Microsoft.AspNetCore.Mvc;
using MOD.TechnologyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOD.TechnologyService.Helpers
{
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(TechnologyResponse faultResponse) 
            : base(faultResponse)
        {
            StatusCode = 500;
        }
    }
}
