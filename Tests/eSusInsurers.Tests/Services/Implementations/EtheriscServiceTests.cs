using System.Net;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using eSusInsurers.Infrastructure.Repositories;
using eSusInsurers.Models.Etherisc.Policy;
using eSusInsurers.Services.Implementations;
using eSusInsurers.Tests.Utilities;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Xunit;

namespace eSusInsurers.Tests.Services.Implementations;


public class EtheriscServiceTests
{
    
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IEtheriscPolicyRepository> _etheriscPolicyRepository;
    private readonly Mock<FarmerRepository> _farmerRepository;
    private readonly ILogger<EtheriscService> _logger;
    private EtheriscService _etheriscService;
    private readonly HttpClient _httpClient;
    private readonly MockHttpMessageHandler _mockHttp;

    public EtheriscServiceTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _etheriscPolicyRepository = new Mock<IEtheriscPolicyRepository>();
        _unitOfWork.Setup(x => x.EtheriscPolicyRepository).Returns(_etheriscPolicyRepository.Object);
        _logger = new Logger<EtheriscService>(new LoggerFactory());
        _farmerRepository = new Mock<FarmerRepository>();
    }


    [Fact]
    public async Task CreatePolicyMetadata_CreatesPolicyMetadata()
    {

                var handlerMock = new Mock<HttpMessageHandler>();
        var createdRisk =
            "{\n  \"configId\": \"7Zv4TZoBLxUi\",\n  \"createdAt\": 1700316957,\n  \"crop\": \"coffee\",\n  \"deductible\": 0,\n  \"draughtLoss\": 0.27,\n  \"endOfSeason\": \"2025-06-15\",\n  \"excessRainfallLoss\": 0.05,\n  \"finalPayout\": 0.27,\n  \"isValid\": true,\n  \"locationId\": \"kDho7606IRdr\",\n  \"payout\": 0.27,\n  \"startOfSeason\": \"2025-01-20\",\n  \"totalLoss\": 0.27,\n  \"updatedAt\": 1700316957\n}";
        var createdLocation = "{\n  \"_id\": \"kDho7606IRdr\",\n  \"coordinatesLevel\": \"VILLAGE\",\n  \"country\": \"UG\",\n  \"district\": \"MASAKA\",\n  \"latitude\": -0.4365,\n  \"longitude\": 31.678,\n  \"openstreetmap\": [\n    \"https://www.openstreetmap.org/#map=14/-0.4365/31.6780\"\n  ],\n  \"subcounty\": \"Kabonera\",\n  \"village\": \"Kiziba\",\n  \"zone\": \"Central\"\n}";
        var createdConfig = "{\n  \"_id\": \"7Zv4TZoBLxUi\",\n  \"endOfSeason\": \"2025-06-30\",\n  \"name\": \"2025 First Seasons\",\n  \"seasonDays\": 120,\n  \"startOfSeason\": \"2025-01-15\",\n  \"year\": 2025\n}";
        var createdPerson = "{\n  \"externalId\": \"PRS1234\",\n  \"firstName\": \"Florence\",\n  \"gender\": \"f\",\n  \"id\": \"fXJ6Gwfgnw-C\",\n  \"lastName\": \"Auma\",\n  \"locationId\": \"U6ufadiIe0Xz\",\n  \"mobilePhone\": \"+25656234567\",\n  \"tx\": \"0x10cc6457d494d0fee7aeb89c63bcdd98f90aad18bb761591d8da1314551ca3ca\",\n  \"wallet\": \"0x03507c8a16513F1615bD4a00BDD4570514a6ef21\",\n  \"walletIndex\": [\n    2345\n  ]\n}";
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsoluteUri.Contains("risk")),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(CreateResponse(createdRisk));
        
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsoluteUri.Contains("location")),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(CreateResponse(createdLocation));
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsoluteUri.Contains("config")),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(CreateResponse(createdConfig));
        
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsoluteUri.Contains("person")),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(CreateResponse(createdPerson));
        
        var httpClient = new HttpClient(handlerMock.Object) {
            BaseAddress = new Uri("https://api-test.esusfarm.etherisc.com/")
        };
        _etheriscService = new EtheriscService(_unitOfWork.Object, _logger, httpClient);
        var result = await _etheriscService.AddPolicyMetadata(createAddPolicyRequest(), new CancellationToken());
        Console.WriteLine(result);
    }
    
    private HttpResponseMessage CreateResponse(string content)
    {
        var response = new HttpResponseMessage { StatusCode =HttpStatusCode.OK };
        if (!string.IsNullOrEmpty(content))
        {
            response.Content = new StringContent(content);
        }
        return response;
    }

    private AddPolicyRequest createAddPolicyRequest()
    {
        var addPolicyRequest = new AddPolicyRequest
        {
            PolicyNumber = "",
            Configuration = new Configuration
            {
                createddAt = 20250115,
                endOfSeason = "2025-06-15",
                isValid = true,
                franchise = 1,
                name = "test",
                seasonDays = 0,
                startOfSeason = "2025-01-20",
                updatedAt = 1700316957,
                year = 2025
            },
            Location = new Location
            {
                CoordinatesLevel = "",
                Country = "UG",
                District = "MASAKA",
                Lattitude = 98.0,
                Longitude = 18.0,
                OpenStreetMap = "https://www.openstreetmap.org/",
                SubCountry = "Bulisa",
                Village = "test",
                Zone = ""
            },
            Person = new Person
            {
                externalId = "1",
                firstName = "test",
                lastName = "testy",
                gender = "f",
                locationId = "",
                mobilePhone = "+25656234568",
                wallet = "",
            },
            InsuredAmount = 707080,
            PremiumAmount = 7907980,
            Crop = "maize"
        };
        return addPolicyRequest;
    }
    
    
}