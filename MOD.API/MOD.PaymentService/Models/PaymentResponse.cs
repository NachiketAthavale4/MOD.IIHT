using System.Collections.Generic;

namespace MOD.PaymentService.Models
{
    public class PaymentResponse
    {
        public IEnumerable<Payment> PaymentList { get; set; }
        public Fault Fault { get; set; }
        public bool SuccessIndicator { get; set; }
    }
}
