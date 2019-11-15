using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOD.TechnologyService.Models
{
    public class Fault
    {
        public string FaultMessage { get; set; }
        public string InnerMessage { get; set; }
        public string FaultSource { get; set; }
    }
}
