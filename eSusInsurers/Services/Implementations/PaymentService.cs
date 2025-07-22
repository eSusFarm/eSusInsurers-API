using AutoMapper;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Models.Payment;
using eSusInsurers.Services.Interfaces;

namespace eSusInsurers.Services.Implementations;

public class PaymentService(IUnitOfWork unitOfWork, IMapper mapper,  ILogger<PaymentService> logger): IPaymentService
{
    public async Task<RegisterPaymentResponse> registerPayment(RegisterPaymentRequest registerPaymentRequest, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Register Payment Request {request}", registerPaymentRequest);
            InsurancePremiumPayment premiumPayment = new InsurancePremiumPayment
            {
                CropInsuranceId = registerPaymentRequest.CropInsuranceId,
                PaidAmount = registerPaymentRequest.PaidAmount,
                TaxAmount = registerPaymentRequest.TaxAmount,
                TotalPaidAmount = registerPaymentRequest.TotalPaidAmount,
                Currency = registerPaymentRequest.currency,
                ModeOfPayment = registerPaymentRequest.ModeOfPayment,
                Status = "COMPLETED",
                IsActive = true,
                PaymentDate = DateTime.Now,
                policyNumber = registerPaymentRequest.policyNumber,
            };
            
            logger.LogInformation("Create premium payment.");
            
            premiumPayment = createPremiumPayment(premiumPayment, cancellationToken).GetAwaiter().GetResult();
            RegisterPaymentResponse response = new RegisterPaymentResponse
            {
                message = "SUCCESS",
                statusCode = 200,
                CropInsuranceId = registerPaymentRequest.CropInsuranceId,
                PremiumPaymentId = premiumPayment.Id
            };
            logger.LogInformation("Create premium payment successfully.");
            return response;
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            RegisterPaymentResponse response = new RegisterPaymentResponse
            {
                message = "ERROR : " + e.Message,
                statusCode = 500,
            };
            logger.LogInformation("Error creating premium payment.");
            return response;
        }
    }
    
    private async Task<InsurancePremiumPayment> createPremiumPayment(InsurancePremiumPayment premiumPayment,CancellationToken cancellationToken)
    {
        var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var entity = await unitOfWork.PremiumPaymentsRepository.AddAsync(premiumPayment, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            logger.LogInformation("Premium Payment created Successfully.");
            return entity;
        }
        catch (Exception ex)
        {
            
            logger.LogError(new System.Diagnostics.StackTrace().ToString());
            logger.LogError(ex.InnerException.Message);
            throw new Exception(ex.InnerException.Message);
        }
    }
}