using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOD.SearchService.Models
{
    public class SearchResponse
    {
        public IEnumerable<Training> TrainingsList { get; set; }
        public Fault Fault { get; set; }
        public bool SuccessIndicator { get; set; }
    }
}
