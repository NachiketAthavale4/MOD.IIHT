using System.Collections.Generic;

namespace MOD.TrainingService.Models
{
    public class TrainingResponse
    {
        public IEnumerable<Training> TrainingsList { get; set; }
        public Fault Fault { get; set; }
        public bool SuccessIndicator { get; set; }
    }
}
