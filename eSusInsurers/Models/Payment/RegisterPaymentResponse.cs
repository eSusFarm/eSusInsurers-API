namespace eSusInsurers.Models.Payment
{
    public class RegisterPaymentResponse
    {
        public string message { get; set; }
        public int statusCode { get; set; }
        public int CropInsuranceId { get; set; }
        public int PremiumPaymentId { get; set; }
    }
}

