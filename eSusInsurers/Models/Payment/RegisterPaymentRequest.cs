namespace eSusInsurers.Models.Payment
{
    public class RegisterPaymentRequest
    {
        public int CropInsuranceId { get; set; }
        public String policyNumber { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalPaidAmount { get; set; }
        public String currency  { get; set; }
        public String ModeOfPayment { get; set; }
        public String Status  { get; set; }
    }
}

