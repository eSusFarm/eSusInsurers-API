using System.Net;
using Moq;
using Moq.Protected;

namespace eSusInsurers.Tests.Utilities;

public class MockHttpMessageHandler
{
    public Mock<HttpMessageHandler> SetupReturn()
    {
        var handlerMock = new Mock<HttpMessageHandler>();
        var createdRisk =
            "{\n  \"configId\": \"7Zv4TZoBLxUi\",\n  \"createdAt\": 1700316957,\n  \"crop\": \"coffee\",\n  \"deductible\": 0,\n  \"draughtLoss\": 0.27,\n  \"endOfSeason\": \"2025-06-15\",\n  \"excessRainfallLoss\": 0.05,\n  \"finalPayout\": 0.27,\n  \"isValid\": true,\n  \"locationId\": \"kDho7606IRdr\",\n  \"payout\": 0.27,\n  \"startOfSeason\": \"2025-01-20\",\n  \"totalLoss\": 0.27,\n  \"updatedAt\": 1700316957\n}";
        var createdLocation = "{\n  \"_id\": \"kDho7606IRdr\",\n  \"coordinatesLevel\": \"VILLAGE\",\n  \"country\": \"UG\",\n  \"district\": \"MASAKA\",\n  \"latitude\": -0.4365,\n  \"longitude\": 31.678,\n  \"openstreetmap\": [\n    \"https://www.openstreetmap.org/#map=14/-0.4365/31.6780\"\n  ],\n  \"subcounty\": \"Kabonera\",\n  \"village\": \"Kiziba\",\n  \"zone\": \"Central\"\n}";
        var createdConfig = "{\n  \"_id\": \"7Zv4TZoBLxUi\",\n  \"endOfSeason\": \"2025-06-30\",\n  \"name\": \"2025 First Seasons\",\n  \"seasonDays\": 120,\n  \"startOfSeason\": \"2025-01-15\",\n  \"year\": 2025\n}";
        var createdPerson = "{\n  \"externalId\": \"PRS1234\",\n  \"firstName\": \"Florence\",\n  \"gender\": \"f\",\n  \"id\": \"fXJ6Gwfgnw-C\",\n  \"lastName\": \"Auma\",\n  \"locationId\": \"U6ufadiIe0Xz\",\n  \"mobilePhone\": \"+25656234567\",\n  \"tx\": \"0x10cc6457d494d0fee7aeb89c63bcdd98f90aad18bb761591d8da1314551ca3ca\",\n  \"wallet\": \"0x03507c8a16513F1615bD4a00BDD4570514a6ef21\",\n  \"walletIndex\": [\n    2345\n  ]\n}";
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "PostAsync",
                ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsoluteUri.Contains("risk")),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(CreateResponse(createdRisk));
        
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "PostAsync",
                ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsoluteUri.Contains("location")),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(CreateResponse(createdLocation));
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "PostAsync",
                ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsoluteUri.Contains("config")),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(CreateResponse(createdConfig));
        
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "PostAsync",
                ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsoluteUri.Contains("person")),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(CreateResponse(createdPerson));
        return handlerMock;
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
}