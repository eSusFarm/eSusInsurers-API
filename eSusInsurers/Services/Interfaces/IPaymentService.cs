using eSusInsurers.Models.Payment;

namespace eSusInsurers.Services.Interfaces
{
    public interface IPaymentService
    {
     Task<RegisterPaymentResponse> registerPayment(RegisterPaymentRequest registerPaymentRequest, CancellationToken cancellationToken);
    } 
}

