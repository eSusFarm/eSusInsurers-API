using eSusInsurers.Models.Etherisc;
using eSusInsurers.Models.Etherisc.Config;
using eSusInsurers.Models.Etherisc.Location;
using eSusInsurers.Models.Etherisc.Person;
using eSusInsurers.Services.Interfaces;

namespace eSusInsurers.Services.Implementations;

public class EtheriscService: IEtheriscService
{
    private readonly String _etheriscBaseUrl = "https://api.esusfarm.etherisc.com/";
    
    
    
    /// <summary>
    /// Create new Etherisc Policy.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<long> AddPolicy(AddPolicyRequest request, CancellationToken cancellationToken)
    {
        /*
         * 1. Create instance of InsuranceRisk
         * 2. Call Etherisc and create location
         * 3. Call Etherisc and create Configuration
         * 4. Call Etherisc and create Person
         * 5. Call Etherisc and create Policy
         */
        
        // Sample Calling and accessing result from method, for your perusal.
        Task<LocationResponse?> locationResponseTask = createLocation(new LocationRequest());
        LocationResponse? locationResponse = locationResponseTask.GetAwaiter().GetResult();
        
        //TODO Remove.
        throw new NotImplementedException();
    }

    //create location on etherisc here.
    private async Task<LocationResponse?> createLocation(LocationRequest locationRequest)
    {
        HttpClient client = new HttpClient();
        try
        {
            LocationResponse? locationResponse = null;
            client.DefaultRequestHeaders.Add("accept","application/json");      
            client.DefaultRequestHeaders.Add("Content-Type","application/json");      
            HttpResponseMessage response = await client.PostAsJsonAsync(_etheriscBaseUrl.Concat("location").ToString(), locationRequest);

            if (response.IsSuccessStatusCode)
            {
                locationResponse = await response.Content.ReadFromJsonAsync<LocationResponse>();
                return locationResponse;
            }
            throw new Exception("Response Status is not 200!");
        }
        catch (Exception ex)
        {
         Console.WriteLine(ex.Message);
         Console.WriteLine("Unable to create location!");
         throw new Exception("Unable to create location!");
        }
    }
    
    //create configuration on etherisc here.
    private async Task<ConfigResponse?> CreateConfiguration(ConfigRequest configRequest)
    {
        HttpClient client = new HttpClient();
        try
        {
            ConfigResponse? configResponse = null;
            client.DefaultRequestHeaders.Add("accept","application/json");      
            client.DefaultRequestHeaders.Add("Content-Type","application/json");      
            HttpResponseMessage response = await client.PostAsJsonAsync(_etheriscBaseUrl.Concat("location").ToString(), configRequest);
            if (response.IsSuccessStatusCode)
            {
                configResponse = await response.Content.ReadFromJsonAsync<ConfigResponse>();
                return configResponse;
            }
            throw new Exception("Response Status is not 200!");
        }
        catch (Exception ex)
        {
         Console.WriteLine(ex.Message);
         Console.WriteLine("Unable to create Configuration!");
         throw new Exception("Unable to create Configuration!");
        }
    }
    
    //create person on etherisc here.
    private async Task<PersonResponse?> CreatePerson(PersonRequest personRequest)
    {
        HttpClient client = new HttpClient();
        try
        {
            PersonResponse? personResponse = null;
            client.DefaultRequestHeaders.Add("accept","application/json");      
            client.DefaultRequestHeaders.Add("Content-Type","application/json");      
            HttpResponseMessage response = await client.PostAsJsonAsync(_etheriscBaseUrl.Concat("location").ToString(), personRequest);
            if (response.IsSuccessStatusCode)
            {
                personResponse = await response.Content.ReadFromJsonAsync<PersonResponse>();
                return personResponse;
            }
            throw new Exception("Response Status is not 200!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("Unable to create Person!");
            throw new Exception("Unable to create Person!");
        }
    }
}