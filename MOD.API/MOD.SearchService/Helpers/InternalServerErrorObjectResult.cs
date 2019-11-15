using Microsoft.AspNetCore.Mvc;
using MOD.SearchService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOD.SearchService.Helpers
{
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(SearchResponse faultResponse) 
            : base(faultResponse)
        {
            StatusCode = 500;
        }
    }
}
