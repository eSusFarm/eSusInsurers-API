using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Models.EsusFarm;
using eSusInsurers.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace eSusInsurers.Services.Implementations
{
   public class EsusFarmPolicyService : IEsusFarmPolicyService
{
    private readonly HttpClient _httpClient;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<EsusFarmPolicyService> _logger;

    public EsusFarmPolicyService(
        HttpClient httpClient,
        IUnitOfWork unitOfWork,
        ILogger<EsusFarmPolicyService> logger)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<bool> ProcessPolicyRequestAsync(EsusFarmPolicyRequestDto request, CancellationToken cancellationToken)
    {
        try
        {
            var jsonContent = JsonSerializer.Serialize(request);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://api.esusfarm.etherisc.com/policy/", httpContent, cancellationToken);

            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
            var isSuccess = response.IsSuccessStatusCode;

            var esusFarmPolicy = new EsusFarmPolicy
            {
                ExternalId = request.externalId,
                PolicyId = request.id,
                OnchainId = request.onchainId,
                PersonId = request.personId,
                PremiumAmount = request.premiumAmount,
                RiskId = request.riskId,
                SubscriptionDate = request.subscriptionDate,
                SumInsuredAmount = request.sumInsuredAmount,
                ResponseData = responseContent,
                IsSuccess = isSuccess
            };

            if (!isSuccess)
            {
                _logger.LogWarning("Unsuccessful response from EsusFarm API: {ResponseContent}", responseContent);
                return false;
            }
            else
            {
                await _unitOfWork.EsusFarmPolicyRepository.AddAsync(esusFarmPolicy, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                _logger.LogWarning("Successful response from EsusFarm API, persistence complete: {ResponseContent}", responseContent);
               return isSuccess;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while processing EsusFarm policy request");
            throw new Exception("Policy request processing failed. Check logs for details.");
        }
    }
}
}

