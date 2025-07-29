using System.Net.Http.Headers;
using System.Text;
using AutoMapper;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Domain.Models;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using eSusInsurers.Models.Etherisc;
using eSusInsurers.Models.Etherisc.Config;
using eSusInsurers.Models.Etherisc.Location;
using eSusInsurers.Models.Etherisc.Person;
using eSusInsurers.Models.Etherisc.Policy;
using eSusInsurers.Services.Interfaces;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Crop = eSusInsurers.Domain.Models.Crop;
using CropInsurance = eSusInsurers.Domain.Entities.CropInsurance;
using Farmer = eSusInsurers.Domain.Entities.Farmer;
using FarmerCrop = eSusInsurers.Domain.Entities.FarmerCrop;
using InsurancePolicy = eSusInsurers.Domain.Entities.InsurancePolicy;
using InsuranceRequest = eSusInsurers.Domain.Entities.InsuranceRequest;
using InsuranceRisk = eSusInsurers.Domain.Entities.InsuranceRisk;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Season = eSusInsurers.Domain.Entities.Season;

namespace eSusInsurers.Services.Implementations;

public class EtheriscService(IUnitOfWork unitOfWork, IMapper mapper,  ILogger<EtheriscService> logger, HttpClient _httpClient ): IEtheriscService
{
    private readonly String _etheriscBaseUrl = "https://api-test.esusfarm.etherisc.com/";
    
    
    
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
            
            // Create Farmer
            // Create Farmer Crops??
            // Create insurance policies
            // Create insurance risk
            // Create crop policy.
            AddPolicyResponse addPolicyResponse = new AddPolicyResponse();
            Farmer farmer = new Farmer
            {
                Name = request.Person.firstName,
                Surname = request.Person.lastName,
                MobileNumber = Convert.ToDecimal(request.Person.mobilePhone)
            };
            farmer = createFarmer(farmer, cancellationToken).GetAwaiter().GetResult();


            Domain.Entities.Crop? crop = unitOfWork.CropRepository.GetCropByName(request.Crop, cancellationToken).GetAwaiter().GetResult();

            if (crop == null)
            {
                throw new Exception("Crop " + request.Crop + " not found.");
            }
            
            FarmerCrop farmerCrop = new FarmerCrop
            {
                CropId = crop.Id,
                FarmerId = farmer.Id,
            };
            farmerCrop = createFarmerCrop(farmerCrop, cancellationToken).GetAwaiter().GetResult();
            var insuranceProviders = unitOfWork.InsuranceProviderRepository.GetAll(new string[]{}, true);

            if (!insuranceProviders.Any())
            {
                logger.LogError("Insurance provider not found.");
                addPolicyResponse.message = "Insurance provider not found.";
                addPolicyResponse.statusCode = 500;
                return addPolicyResponse;
            }
            
            logger.LogInformation("Creating new insurance policy.");
            InsurancePolicy insurancePolicies = new InsurancePolicy
            {
                PolicyNumber = request.PolicyNumber,
                PolicyName = "AIC " + request.PolicyNumber,
                IsActive = true,
                InsuranceProviderId = insuranceProviders.FirstOrDefault().Id,
                CreatedBy = "watson@esusfarm.africa",
                CreatedDate = DateTime.Now,
            };
            insurancePolicies = createInsurancePolicy(insurancePolicies, cancellationToken).GetAwaiter().GetResult();
            logger.LogInformation("Season Name = {} Season Year = {} ", request.Configuration.name, request.Configuration.year.ToString());
            var selectedSeason = unitOfWork.SeasonRepository.GetBySeasonNameAsync(request.Configuration.name, request.Configuration.year.ToString(), cancellationToken).GetAwaiter().GetResult();
            if (selectedSeason == null)
            {
                logger.LogError("Season " + request.Configuration.name + " not found.");
                addPolicyResponse.message = "Season " + request.Configuration.name + " not found.";
                addPolicyResponse.statusCode = 500;
                return addPolicyResponse;
            }

            var selectedLocation =  unitOfWork.LocationsRepository.GetAll();
            if (!selectedLocation.Any())
            {
                logger.LogError("No location found.");
                addPolicyResponse.message = "No Locations Found!";
                addPolicyResponse.statusCode = 500;
                return addPolicyResponse;
            }
            
            InsuranceRisk insuranceRisk = new InsuranceRisk
            {
                InsurancePolicyId = insurancePolicies.Id,
                LocationId = selectedLocation.FirstOrDefault().Id,
                SeasonId = selectedSeason.Id,
                CropName = request.Crop
            };
            
            insuranceRisk = createInsuranceRisk(insuranceRisk, cancellationToken).GetAwaiter().GetResult();

            CropInsurance cropInsurance = new CropInsurance
            {
                FarmerId = farmer.Id,
                InsurancePolicyId = insurancePolicies.Id,
                InsuranceRiskId = insuranceRisk.Id,
                Status = "Active",
                IsActive = true,
                FarmerCropId = farmerCrop.Id,
                CropName = request.Crop
            };

            cropInsurance = createCropInsurance(cropInsurance, cancellationToken).GetAwaiter().GetResult();

            
            logger.LogInformation("Creating Insurance Request");
            InsuranceRequest insuranceRequest = new InsuranceRequest
            {
                FarmerName = request.Person.firstName + " " + request.Person.lastName,
                FarmerId = farmer.Id,
                FarmerCropId = farmerCrop.Id,
                CropName = request.Crop,
                InsuranceCompanyName = "AIC",
                IsActive = true,
                IsRequestSubmitted = true,
                HomeLocationVillage = request.Location.Village,
                FarmLocationVillage = request.Location.Village,
                FarmLocationSubCounty  = request.Location.Country,
                CreatedBy = "USSD API",
                HomeLocationDistrict = request.Location.District,
                FarmLocationDistrict = request.Location.District,
                InsuredAmount = (decimal) request.InsuredAmount,
                PremiumAmount = (decimal) request.PremiumAmount,
                ProgramName = "",
            };

            await createInsuranceRequest(insuranceRequest, cancellationToken);
            
            
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
            if (locationResponse != null)
            {
                etheriscPolicy.locationId = locationResponse.id;
            }
            else
            {
                logger.LogError("Location Response is null.");
                addPolicyResponse.message = "Location Response is null.";
                addPolicyResponse.statusCode = 500;
                return addPolicyResponse;
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
            logger.LogInformation(configResponse.ToString());
            if (configResponse != null)
            {
                etheriscPolicy.configId = configResponse.id;
            }
            else
            {
                logger.LogError("Config Response is null.");
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
            logger.LogInformation(personResponse.ToString());
            if (personResponse != null)
            {
                etheriscPolicy.personId = personResponse.id;
            }
            else
            {
                logger.LogError("Person Response is null.");
                addPolicyResponse.message = "Person Response is null.";
                addPolicyResponse.statusCode = 500;
                return addPolicyResponse;
            }
            
            // Create Risk
            RiskRequest riskRequest = new RiskRequest
            {
                deductible = 0,
                isValid = true,
                startOfSeason = request.Configuration.startOfSeason,
                endOfSeason = request.Configuration.endOfSeason,
                crop = request.Crop,
                locationId = locationResponse.id,
                configId = configResponse.id
            };

            RiskResponse riskResponse = CreateRisk(riskRequest).GetAwaiter().GetResult();
            logger.LogInformation(riskResponse.ToString());
            if (riskResponse != null)
            {
                etheriscPolicy.riskId = riskResponse.id;
            }
            else
            {
                logger.LogError("Risk Response is null.");
                addPolicyResponse.message = "Risk Response is null.";
                addPolicyResponse.statusCode = 500;
                return addPolicyResponse;
            }
            
            CreatePolicyRequest policyRequest = new CreatePolicyRequest
            {
                externalId = Guid.NewGuid().ToString(),
                personId = personResponse.id,
                riskId = riskResponse.id,
                subscriptionDate =DateTime.Today.AddDays(1).ToString("yyyy-MM-dd"),
                sumInsuredAmount = request.InsuredAmount,
                premiumAmount = request.PremiumAmount,
                onchainId = Guid.NewGuid().ToString()
            };

            CreatePolicyResponse policyResponse =  CreatePolicyOnBlockchain(policyRequest).GetAwaiter().GetResult();
            logger.LogInformation("Policy response Final {policyresponse}", policyResponse);
            if (policyResponse != null)
            {
                logger.LogInformation("Syncing Policy Request {etheriscPolicy} and Policy {policyResponse}", etheriscPolicy, policyResponse);
                etheriscPolicy.policyId = policyResponse.id;
                await SyncPolicy(policyResponse.id);
            }
            else
            {
                logger.LogError("CreatePolicy Response is null.");
                addPolicyResponse.message = "CreatePolicy Response is null.";
                addPolicyResponse.statusCode = 500;
                return addPolicyResponse;
            }
            
            var policy = await SaveEtheriscPolicy(etheriscPolicy, cancellationToken);
            logger.LogInformation("Created etherisc policy successfully.");
            addPolicyResponse.message = "Created etherisc policy successfully.";
            addPolicyResponse.statusCode = 200;
            addPolicyResponse.policyId = policyResponse.id;
            addPolicyResponse.riskId = riskResponse.id;
            addPolicyResponse.cropInsuranceId = cropInsurance.Id;
            return addPolicyResponse;
        }
        catch (Exception ex)
        {
            logger.LogInformation(ex.Message);
            logger.LogInformation("Something went wrong!");
            throw new Exception(ex.Message);
        }
    }

    public async Task<AddPolicyResponse> AddPolicyMetadata(AddPolicyRequest request, CancellationToken cancellationToken)
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
            
            // Create Farmer
            // Create Farmer Crops??
            // Create insurance policies
            // Create insurance risk
            // Create crop policy.
            AddPolicyResponse addPolicyResponse = new AddPolicyResponse();
            Farmer farmer = new Farmer
            {
                Name = request.Person.firstName,
                Surname = request.Person.lastName,
                MobileNumber = Convert.ToDecimal(request.Person.mobilePhone)
            };
            farmer = createFarmer(farmer, cancellationToken).GetAwaiter().GetResult();


            Domain.Entities.Crop? crop = unitOfWork.CropRepository.GetCropByName(request.Crop, cancellationToken).GetAwaiter().GetResult();

            if (crop == null)
            {
                throw new Exception("Crop " + request.Crop + " not found.");
            }
            
            FarmerCrop farmerCrop = new FarmerCrop
            {
                CropId = crop.Id,
                FarmerId = farmer.Id,
            };
            farmerCrop = createFarmerCrop(farmerCrop, cancellationToken).GetAwaiter().GetResult();
            var insuranceProviders = unitOfWork.InsuranceProviderRepository.GetAll(new string[]{}, true);

            if (!insuranceProviders.Any())
            {
                logger.LogError("Insurance provider not found.");
                addPolicyResponse.message = "Insurance provider not found.";
                addPolicyResponse.statusCode = 500;
                return addPolicyResponse;
            }
            
            logger.LogInformation("Creating new insurance policy.");
            InsurancePolicy insurancePolicies = new InsurancePolicy
            {
                PolicyNumber = request.PolicyNumber,
                PolicyName = "AIC " + request.PolicyNumber,
                IsActive = true,
                InsuranceProviderId = insuranceProviders.FirstOrDefault().Id,
                CreatedBy = "watson@esusfarm.africa",
                CreatedDate = DateTime.Now,
            };
            insurancePolicies = createInsurancePolicy(insurancePolicies, cancellationToken).GetAwaiter().GetResult();
            logger.LogInformation("Season Name = {} Season Year = {} ", request.Configuration.name, request.Configuration.year.ToString());
            var selectedSeason = unitOfWork.SeasonRepository.GetBySeasonNameAsync(request.Configuration.name, request.Configuration.year.ToString(), cancellationToken).GetAwaiter().GetResult();
            if (selectedSeason == null)
            {
                logger.LogError("Season " + request.Configuration.name + " not found.");
                addPolicyResponse.message = "Season " + request.Configuration.name + " not found.";
                addPolicyResponse.statusCode = 500;
                return addPolicyResponse;
            }

            var selectedLocation =  unitOfWork.LocationsRepository.GetAll();
            if (!selectedLocation.Any())
            {
                logger.LogError("No location found.");
                addPolicyResponse.message = "No Locations Found!";
                addPolicyResponse.statusCode = 500;
                return addPolicyResponse;
            }
            
            InsuranceRisk insuranceRisk = new InsuranceRisk
            {
                InsurancePolicyId = insurancePolicies.Id,
                LocationId = selectedLocation.FirstOrDefault().Id,
                SeasonId = selectedSeason.Id,
                CropName = request.Crop
            };
            
            insuranceRisk = createInsuranceRisk(insuranceRisk, cancellationToken).GetAwaiter().GetResult();

            CropInsurance cropInsurance = new CropInsurance
            {
                FarmerId = farmer.Id,
                InsurancePolicyId = insurancePolicies.Id,
                InsuranceRiskId = insuranceRisk.Id,
                Status = "Active",
                IsActive = true,
                FarmerCropId = farmerCrop.Id,
                CropName = request.Crop
            };

            cropInsurance = createCropInsurance(cropInsurance, cancellationToken).GetAwaiter().GetResult();

            
            logger.LogInformation("Creating Insurance Request");
            InsuranceRequest insuranceRequest = new InsuranceRequest
            {
                FarmerName = request.Person.firstName + " " + request.Person.lastName,
                FarmerId = farmer.Id,
                FarmerCropId = farmerCrop.Id,
                CropName = request.Crop,
                InsuranceCompanyName = "AIC",
                IsActive = true,
                IsRequestSubmitted = true,
                HomeLocationVillage = request.Location.Village,
                FarmLocationVillage = request.Location.Village,
                FarmLocationSubCounty  = request.Location.Country,
                CreatedBy = "USSD API",
                HomeLocationDistrict = request.Location.District,
                FarmLocationDistrict = request.Location.District,
                InsuredAmount = (decimal) request.InsuredAmount,
                PremiumAmount = (decimal) request.PremiumAmount,
                ProgramName = "",
            };

            await createInsuranceRequest(insuranceRequest, cancellationToken);
            
            
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
            if (locationResponse != null)
            {
                etheriscPolicy.locationId = locationResponse.id;
            }
            else
            {
                logger.LogError("Location Response is null.");
                addPolicyResponse.message = "Location Response is null.";
                addPolicyResponse.statusCode = 500;
                return addPolicyResponse;
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
            logger.LogInformation(configResponse.ToString());
            if (configResponse != null)
            {
                etheriscPolicy.configId = configResponse.id;
            }
            else
            {
                logger.LogError("Config Response is null.");
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
            logger.LogInformation(personResponse.ToString());
            if (personResponse != null)
            {
                etheriscPolicy.personId = personResponse.id;
            }
            else
            {
                logger.LogError("Person Response is null.");
                addPolicyResponse.message = "Person Response is null.";
                addPolicyResponse.statusCode = 500;
                return addPolicyResponse;
            }
            
            // Create Risk
            RiskRequest riskRequest = new RiskRequest
            {
                deductible = 0,
                isValid = true,
                startOfSeason = request.Configuration.startOfSeason,
                endOfSeason = request.Configuration.endOfSeason,
                crop = request.Crop,
                locationId = locationResponse.id,
                configId = configResponse.id
            };

            RiskResponse riskResponse = CreateRisk(riskRequest).GetAwaiter().GetResult();
            logger.LogInformation(riskResponse.ToString());
            if (riskResponse != null)
            {
                etheriscPolicy.riskId = riskResponse.id;
            }
            else
            {
                logger.LogError("Risk Response is null.");
                addPolicyResponse.message = "Risk Response is null.";
                addPolicyResponse.statusCode = 500;
                return addPolicyResponse;
            }
            
            var policy = await SaveEtheriscPolicy(etheriscPolicy, cancellationToken);
            logger.LogInformation("Created etherisc policy successfully.");
            addPolicyResponse.message = "Created etherisc policy successfully.";
            addPolicyResponse.statusCode = 200;
            addPolicyResponse.policyId = null;
            addPolicyResponse.riskId = riskResponse.id;
            addPolicyResponse.cropInsuranceId = cropInsurance.Id;
            return addPolicyResponse;
        }
        catch (Exception ex)
        {
            logger.LogInformation(ex.Message);
            logger.LogInformation("Something went wrong!");
            throw new Exception(ex.Message);
        }
    }

    public async Task<AddPolicyResponse> CreateBlockChainPolicy(AddPolicyRequest request, CancellationToken cancellationToken)
    {
        try
        {
            AddPolicyResponse addPolicyResponse = new AddPolicyResponse();
            logger.LogInformation("Creating etherisc policy.");
            logger.LogInformation("Find Policy By AIC Policy Number");
            var EtheriscPolicy = await unitOfWork.EtheriscPolicyRepository.GetByPolicyNumber(request.PolicyNumber, cancellationToken);
            if (EtheriscPolicy != null)
            {
                CreatePolicyRequest policyRequest = new CreatePolicyRequest
                {
                    externalId = Guid.NewGuid().ToString(),
                    personId = EtheriscPolicy.personId,
                    riskId = EtheriscPolicy.riskId,
                    subscriptionDate =DateTime.Today.AddDays(1).ToString("yyyy-MM-dd"),
                    sumInsuredAmount = request.InsuredAmount,
                    premiumAmount = request.PremiumAmount,
                    onchainId = Guid.NewGuid().ToString()
                };
                var blockchainPolicy = await CreatePolicyOnBlockchain(policyRequest);
                if (blockchainPolicy != null)
                {
                    EtheriscPolicy.policyId = blockchainPolicy.id;
                    UpdateEtheriscPolicy(EtheriscPolicy, cancellationToken);
                    logger.LogInformation("Created etherisc policy successfully.");
                    addPolicyResponse.message = "Created etherisc policy successfully.";
                    addPolicyResponse.statusCode = 200;
                    addPolicyResponse.policyId = null;
                    addPolicyResponse.riskId = EtheriscPolicy.riskId;
                    return addPolicyResponse;
                }
                logger.LogError("Blockchain Policy is null.");
                throw new Exception("Blockchain Policy is null.");
            }
            throw new Exception("EtheriscPolicy is null for Policy Number" + request.PolicyNumber);
        }
        catch (Exception e)
        {
         logger.LogError(e.Message);
         throw new Exception(e.Message);
        }
    }

    public async Task<UpdateRiskResponse> UpdateRisk(UpdateRiskRequest request, CancellationToken cancellationToken)
    {
        try
        {
            HttpClient client=new HttpClient();
            string updateRiskRequestString = JsonConvert.SerializeObject(request);
            logger.LogInformation("Create Location Request : ===== > {request}" , updateRiskRequestString);
            HttpContent content = new StringContent(updateRiskRequestString, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_etheriscBaseUrl + "risk/" + request.id, content);
            string resp = await response.Content.ReadAsStringAsync();
            logger.LogInformation("Create Location Response : ===== > {request}" , resp);
            if (response.IsSuccessStatusCode)
            {
                logger.LogInformation(response.StatusCode + " : " + response.ReasonPhrase);
                var updateRiskResponse = await response.Content.ReadFromJsonAsync<UpdateRiskResponse>();
                logger.LogInformation("Risk Updated successfully.");
                return updateRiskResponse;
            }
            throw new Exception("Unable to update risk!!");
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            throw new Exception(ex.Message);
        }
    }

    public async  Task<string> SyncRisk(string riskId, CancellationToken cancellationToken)
    {
        try
        {
            HttpClient client=new HttpClient();
            HttpContent content = new StringContent("", Encoding.UTF8, "application/json");
            await  client.PostAsync(_etheriscBaseUrl + "risk/" + riskId + "/sync", content);
            return "";
        }
        catch (Exception ex)
        {
            logger.LogError("Unable to sync Risk!!");
            logger.LogError(ex.Message);
            throw new Exception(ex.Message);
        }
    }

    private async Task<EtheriscPolicy> SaveEtheriscPolicy(EtheriscPolicy etheriscPolicy,CancellationToken cancellationToken)
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
    
    private async void UpdateEtheriscPolicy(EtheriscPolicy etheriscPolicy,CancellationToken cancellationToken)
    {
        var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            await unitOfWork.EtheriscPolicyRepository.UpdateAsync(etheriscPolicy, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            logger.LogInformation("Etherisc policy Updated Successfully");
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
    private async Task<CreatePolicyResponse?> CreatePolicyOnBlockchain(CreatePolicyRequest createPolicyRequest)
    {
        try
        {
            HttpClient client=new HttpClient();
            string locationRequestString = JsonConvert.SerializeObject(createPolicyRequest);
            logger.LogInformation("Create Policy Request {request}", locationRequestString);
            HttpContent content = new StringContent(locationRequestString, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_etheriscBaseUrl + "policy/", content);
            string resp = await response.Content.ReadAsStringAsync();
            logger.LogInformation("Create Policy Response {response}", resp);
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
    
    private async Task SyncPolicy(String policyNumber)
    {
        try
        {
            HttpClient client=new HttpClient();
            HttpContent content = new StringContent("", Encoding.UTF8, "application/json");
            client.PostAsync(_etheriscBaseUrl + "policy/" + policyNumber + "/sync", content);
        }
        catch (Exception ex)
        {
            logger.LogError("Unable to sync Policy!!");
            logger.LogError(ex.Message);
            throw new Exception(ex.Message);
        }
    }
    
    private async Task<InsurancePolicy> createInsurancePolicy(InsurancePolicy insurancePolicies,CancellationToken cancellationToken)
    {
        var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var entity = await unitOfWork.InsurancePoliciesRepository.AddAsync(insurancePolicies, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            logger.LogInformation("Created insurance Policy Successfully.");
            return entity;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.InnerException.Message);
            throw new Exception(ex.InnerException.Message);
        }
    }
    
    private async Task<InsuranceRisk> createInsuranceRisk(InsuranceRisk insuranceRisk,CancellationToken cancellationToken)
    {
        var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var entity = await unitOfWork.InsuranceRiskRepository.AddAsync(insuranceRisk, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            logger.LogInformation("Created insurance risk Successfully.");
            return entity;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.InnerException.Message);
            throw new Exception(ex.InnerException.Message);
        }
    }
    
    private async Task<CropInsurance> createCropInsurance(CropInsurance cropInsurance,CancellationToken cancellationToken)
    {
        var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var entity = await unitOfWork.CropInsuranceRepository.AddAsync(cropInsurance, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            logger.LogInformation("Created insurance risk Successfully.");
            return entity;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.InnerException.Message);
            throw new Exception(ex.InnerException.Message);
        }
    }
    
    private async Task<Farmer> createFarmer(Farmer farmer,CancellationToken cancellationToken)
    {
        var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var entity = await unitOfWork.FarmerRepository.AddAsync(farmer, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            logger.LogInformation("Created insurance risk Successfully.");
            return entity;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.InnerException.Message);
            throw new Exception(ex.InnerException.Message);
        }
    }
    
    private async Task<FarmerCrop> createFarmerCrop(FarmerCrop farmer,CancellationToken cancellationToken)
    {
        var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var entity = await unitOfWork.FarmerCroprepository.AddAsync(farmer, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            logger.LogInformation("Created insurance risk Successfully.");
            return entity;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.InnerException.Message);
            throw new Exception(ex.InnerException.Message);
        }
    }
    
    private async Task<InsuranceRequest> createInsuranceRequest(InsuranceRequest insuranceRequest,CancellationToken cancellationToken)
    {
        var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var entity = await unitOfWork.InsuranceRequestsRepository.AddAsync(insuranceRequest, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            logger.LogInformation("Created insurance request Successfully.");
            return entity;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.InnerException.Message);
            throw new Exception(ex.InnerException.Message);
        }
    }
    
    
    
}