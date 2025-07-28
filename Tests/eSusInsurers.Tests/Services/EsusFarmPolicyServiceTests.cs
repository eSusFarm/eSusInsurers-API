using System.Net;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using eSusInsurers.Models.EsusFarm;
using eSusInsurers.Services.Implementations;
using eSusInsurers.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Xunit;

namespace eSusInsurers.Tests.Services;

public class EsusFarmPolicyServiceTests
{
    public readonly HttpClient _HttpClient;
    public readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<ILogger<EsusFarmPolicyService>> _logger;
    private readonly IEsusFarmPolicyService _service;
    private readonly Mock<IEsusFarmPolicyRepository> _repositoryMock;

    public EsusFarmPolicyServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _logger = new Mock<ILogger<EsusFarmPolicyService>>();
        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("Mocked response content")
            });
        _HttpClient = new HttpClient(handlerMock.Object);
        _repositoryMock = new Mock<IEsusFarmPolicyRepository>();
        _unitOfWorkMock.Setup(r=>r.EsusFarmPolicyRepository).Returns(_repositoryMock.Object);
        _service = new EsusFarmPolicyService(_HttpClient, _unitOfWorkMock.Object, _logger.Object);
    }

    [Fact]
    public async Task ProcessPolicyRequest_WhenSuccessfull_ReturnsTrue()
    {
        _repositoryMock.Setup(e => e.AddAsync(It.IsAny<EsusFarmPolicy>(), CancellationToken.None))
            .ReturnsAsync(CreateEsusFarmPolicy());
        var result = await _service.ProcessPolicyRequestAsync(BuildEsusFarmPolicyRequestDto(), new CancellationToken());
        Assert.True(result);
    }
    
    [Fact]
    public async Task ProcessPolicyRequest_WhenThrowException_ReturnsException()
    {
        string expectedException = "Policy request processing failed. Check logs for details.";
        try
        {
            _repositoryMock.Setup(e => e.AddAsync(It.IsAny<EsusFarmPolicy>(), CancellationToken.None))
                .ReturnsAsync(CreateEsusFarmPolicy());
            var result = await _service.ProcessPolicyRequestAsync(null, new CancellationToken());
        } catch (Exception e)
        {
           Assert.Equal(e.Message, expectedException);
        }
    }

    private EsusFarmPolicyRequestDto BuildEsusFarmPolicyRequestDto()
    {
        return  new EsusFarmPolicyRequestDto
        {
            externalId = "1234",
            id = "",
            onchainId = "",
            personId = "",
            premiumAmount = 2324,
            riskId = "",
            subscriptionDate = DateTime.Today
        };
    }

    private EsusFarmPolicy CreateEsusFarmPolicy()
    {
        return new EsusFarmPolicy
        {
            Id = 1,
            ExternalId = "1234"
        };
    }
}