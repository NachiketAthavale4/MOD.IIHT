using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOD.TechnologyService.Models
{
    public class TechnologyResponse
    {
        public IEnumerable<Technology> TechnologyList { get; set; }
        public Fault Fault { get; set; }
        public bool SuccessIndicator { get; set; }
    }
}
