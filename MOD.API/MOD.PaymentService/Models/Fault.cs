namespace MOD.PaymentService.Models
{
    public class Fault
    {
        public string FaultMessage { get; set; }
        public string InnerMessage { get; set; }
        public string FaultSource { get; set; }
    }
}
