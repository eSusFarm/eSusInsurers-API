using System.Net.Http.Headers;
using System.Text;
using AutoMapper;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Domain.Models;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Models.Etherisc;
using eSusInsurers.Models.Etherisc.Config;
using eSusInsurers.Models.Etherisc.Location;
using eSusInsurers.Models.Etherisc.Person;
using eSusInsurers.Models.Etherisc.Policy;
using eSusInsurers.Services.Interfaces;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace eSusInsurers.Services.Implementations;

public class EtheriscService(IUnitOfWork unitOfWork, IMapper mapper,  ILogger<EtheriscService> logger, HttpClient _httpClient): IEtheriscService
{
    private readonly String _etheriscBaseUrl = "https://api.esusfarm.etherisc.com/";
    
    
    
    /// <summary>
    /// Create new Etherisc Policy.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<AddPolicyResponse> AddPolicy(AddPolicyRequest request, CancellationToken cancellationToken)
    {
        /*
         * 1. Create instance of InsuranceRisk
         * 2. Call Etherisc and create location
         * 3. Call Etherisc and create Configuration
         * 4. Call Etherisc and create Person
         * 5. Call Etherisc and create Policy
         */
        try
        {
            logger.LogInformation("Creating Initial Etherisc Entity.");
            EtheriscPolicy etheriscPolicy = new EtheriscPolicy();
            etheriscPolicy.policyNumber = request.PolicyNumber;
            
            logger.LogInformation("Creating Location");
            // Create location
            LocationRequest locationRequest = new LocationRequest
            {
                country = request.Location.Country,
                district = request.Location.District,
                village = request.Location.Village,
                latitude = request.Location.Lattitude,
                longitude = request.Location.Longitude,
                openstreetmap = request.Location.OpenStreetMap,
                coordinatesLevel = request.Location.CoordinatesLevel,
                zone = request.Location.Zone,
                subcounty = request.Location.SubCountry,
                onchainId = String.Empty
            };

            LocationResponse locationResponse = createLocation(locationRequest).GetAwaiter().GetResult();
            Console.WriteLine(locationResponse);
            if (locationResponse != null)
            {
                etheriscPolicy.locationId = locationResponse.id;
            }
            else
            {
                Console.WriteLine("Location Response is null.");
                throw new Exception("Location Response is null!");
            }
            
            
            ConfigRequest configRequest = new ConfigRequest
            {
                franchise = request.Configuration.franchise,
                name = request.Configuration.name,
                year = request.Configuration.year,
                startOfSeason = request.Configuration.startOfSeason,
                endOfSeason = request.Configuration.endOfSeason,
                seasonDays = request.Configuration.seasonDays,
                createdAt = request.Configuration.createddAt,
                updatedAt = request.Configuration.updatedAt,
                isValid = request.Configuration.isValid
            };

            ConfigResponse configResponse = CreateConfiguration(configRequest).GetAwaiter().GetResult();
            Console.WriteLine(configResponse);
            if (configResponse != null)
            {
                etheriscPolicy.configId = configResponse.id;
            }
            else
            {
                Console.WriteLine("Config Response is null.");
                throw new Exception("Config Response is null!");
            }
            
            PersonRequest personRequest = new PersonRequest
            {
                firstName = request.Person.firstName,
                lastName = request.Person.lastName,
                gender = request.Person.gender,
                mobilePhone = request.Person.mobilePhone,
                locationId = locationResponse.id,
                wallet = String.Empty,
                externalId = string.Empty
            };

            PersonResponse personResponse = CreatePerson(personRequest).GetAwaiter().GetResult();
            Console.WriteLine(personResponse);
            if (personResponse != null)
            {
                etheriscPolicy.personId = personResponse.id;
            }
            else
            {
                Console.WriteLine("Person Response is null.");
                throw new Exception("Person Response is null!");
            }
            
            // Create Risk
            RiskRequest riskRequest = new RiskRequest
            {
                deductible = 0,
                isValid = true,
                createdAt = DateTime.UtcNow.ToFileTime(),
                updatedAt = DateTime.UtcNow.ToFileTime(),
                crop = request.Crop,
                locationId = locationResponse.id,
                configId = configResponse.id
            };

            RiskResponse riskResponse = CreateRisk(riskRequest).GetAwaiter().GetResult();
            Console.WriteLine(riskResponse);
            if (riskResponse != null)
            {
                etheriscPolicy.riskId = riskResponse.id;
            }
            else
            {
                Console.WriteLine("Risk Response is null.");
                throw new Exception("Risk Response is null!");
            }
            
            CreatePolicyRequest policyRequest = new CreatePolicyRequest
            {
                externalId = Guid.NewGuid().ToString(),
                personId = personResponse.id,
                riskId = riskResponse.id,
                subscriptionDate = DateTime.Today.ToString("yyyy-MM-dd"),
                sumInsuredAmount = request.InsuredAmount,
                premiumAmount = request.PremiumAmount,
                onchainId = Guid.NewGuid().ToString()
            };

            CreatePolicyResponse policyResponse = CreatePolicy(policyRequest).GetAwaiter().GetResult();
            Console.WriteLine(policyResponse);
            if (policyResponse != null)
            {
                etheriscPolicy.policyId = policyResponse.id;
            }
            else
            {
                Console.WriteLine("CreatePolicy Response is null.");
                throw new Exception("CreatePolicy Response is null!");
            }
            
            createEtheriscPolicy(etheriscPolicy, cancellationToken);
            Console.WriteLine("Created etherisc policy successfully.");
            AddPolicyResponse addPolicyResponse = new AddPolicyResponse
            {
                message = "Created etherisc policy successfully.",
                statusCode = 200,
                policyId=policyResponse.id,
                riskId = riskResponse.id
            };
            return addPolicyResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("Something went wrong!");
            throw new Exception(ex.Message);
        }
    }
    
    private async Task<EtheriscPolicy> createEtheriscPolicy(EtheriscPolicy etheriscPolicy,CancellationToken cancellationToken)
    {
        var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var entity = await unitOfWork.EtheriscPolicyRepository.AddAsync(etheriscPolicy, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            logger.LogInformation("Etherisc policy Updated Successfully");
            return entity;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.InnerException.Message);
            throw new Exception(ex.InnerException.Message);
        }
    }


    private async Task<LocationResponse?> createLocation(LocationRequest locationRequest)
    {
        try
        {
            HttpClient client=new HttpClient();
            string locationRequestString = JsonConvert.SerializeObject(locationRequest);
            logger.LogInformation("Create Location Request : ===== > {request}" , locationRequestString);
            HttpContent content = new StringContent(locationRequestString, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_etheriscBaseUrl + "location/", content);
            string resp = await response.Content.ReadAsStringAsync();
            logger.LogInformation("Create Location Response : ===== > {request}" , resp);
            if (response.IsSuccessStatusCode)
            {
                logger.LogInformation(response.StatusCode + " : " + response.ReasonPhrase);
                var locattionResponse = await response.Content.ReadFromJsonAsync<LocationResponse>();
                logger.LogInformation("Location Created successfully.");
                return locattionResponse;
            }
            throw new Exception("Unable to create Location!!");
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            throw new Exception(ex.Message);
        }
    }
    
    
    
    //create configuration on etherisc here.
    private async Task<ConfigResponse?> CreateConfiguration(ConfigRequest configRequest)
    {
        try
        {
            HttpClient client=new HttpClient();
            string configRequestString = JsonConvert.SerializeObject(configRequest);
            logger.LogInformation("Create Location Request : ===== > {request}" , configRequestString);
            HttpContent content = new StringContent(configRequestString, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_etheriscBaseUrl + "config/", content);
            string resp = await response.Content.ReadAsStringAsync();
            logger.LogInformation("Create Config Response : ===== > {response}" , resp);
            if (response.IsSuccessStatusCode)
            {
                logger.LogInformation(response.StatusCode + " : " + response.ReasonPhrase);
                var configResponse = await response.Content.ReadFromJsonAsync<ConfigResponse>();
                logger.LogInformation("Config Config successfully.");
                return configResponse;
            }
            logger.LogError(response.StatusCode + " : " + response.ReasonPhrase);
            throw new Exception("Unable to create Configuration!!");
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            throw new Exception(ex.Message);
        }
    }
    
    //create person on etherisc here.
    private async Task<PersonResponse?> CreatePerson(PersonRequest personRequest)
    {
        try
        {
            HttpClient client=new HttpClient();
            string personRequestString = JsonConvert.SerializeObject(personRequest);
            HttpContent content = new StringContent(personRequestString, Encoding.UTF8, "application/json");
            logger.LogInformation("Create Person Request : ===== > {request}" , personRequestString);
            var response = await client.PostAsync(_etheriscBaseUrl + "person/", content);
            string resp = await response.Content.ReadAsStringAsync();
            logger.LogInformation("Create Person Response : ===== > {response}" , resp);
            if (response.IsSuccessStatusCode)
            {
                logger.LogInformation(response.StatusCode + " : " + response.ReasonPhrase);
                var personResponse = await response.Content.ReadFromJsonAsync<PersonResponse>();
                logger.LogInformation("Person Created successfully.");
                return personResponse;
            }
            logger.LogError(response.StatusCode + " : " + response.ReasonPhrase);
            throw new Exception("Unable to create Person!!");
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            throw new Exception(ex.Message);
        }
    }
    
    //Create Risk
    private async Task<RiskResponse?> CreateRisk(RiskRequest riskRequest)
    {
        try
        {
            HttpClient client=new HttpClient();
            string riskRequestString = JsonConvert.SerializeObject(riskRequest);
            logger.LogInformation("Create Risk Request : ===== > {request}" , riskRequestString);
            HttpContent content = new StringContent(riskRequestString, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_etheriscBaseUrl + "risk/", content);
            string resp = await response.Content.ReadAsStringAsync();
            logger.LogInformation("Create Risk Response: ======> {response}" , resp);
            if (response.IsSuccessStatusCode)
            {
                logger.LogInformation(response.StatusCode + " : " + response.ReasonPhrase);
                var riskResponse = await response.Content.ReadFromJsonAsync<RiskResponse>();
                logger.LogInformation("Risk Created successfully.");
                return riskResponse;
            }
            logger.LogError(response.StatusCode + " : " + response.ReasonPhrase);
            throw new Exception("Unable to create Risk!!");
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            throw new Exception(ex.Message);
        }
    }
    //Create Policy
    private async Task<CreatePolicyResponse?> CreatePolicy(CreatePolicyRequest createPolicyRequest)
    {
        try
        {
            HttpClient client=new HttpClient();
            string locationRequestString = JsonConvert.SerializeObject(createPolicyRequest);
            logger.LogInformation("Create Policy Request {request}", locationRequestString);
            HttpContent content = new StringContent(locationRequestString, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_etheriscBaseUrl + "policy/", content);
            string resp = await response.Content.ReadAsStringAsync();
            logger.LogInformation("Create Policy Response {request}", resp);
            if (response.IsSuccessStatusCode)
            {
                logger.LogInformation(response.StatusCode + " : " + response.ReasonPhrase);
                var createPolicyResponse = await response.Content.ReadFromJsonAsync<CreatePolicyResponse>();
                return createPolicyResponse;
            }
            logger.LogError(response.StatusCode + " : " + response.ReasonPhrase);
            throw new Exception("Unable to create Policy!!");
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            throw new Exception(ex.Message);
        }
    }
}