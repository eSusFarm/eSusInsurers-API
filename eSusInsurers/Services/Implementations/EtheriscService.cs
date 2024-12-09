using eSusInsurers.Models.Etherisc;
using eSusInsurers.Models.Etherisc.Config;
using eSusInsurers.Models.Etherisc.Person;
using eSusInsurers.Services.Interfaces;

namespace eSusInsurers.Services.Implementations;

public class EtheriscService: IEtheriscService
{
    public Task<long> AddPolicy(AddPolicyRequest request, CancellationToken cancellationToken)
    {
        /*
         * 1. Create instance of InsuranceRisk
         * 2. Call Etherisc and create location
         * 3. Call Etherisc and create Configuration
         * 4. Call Etherisc and create Person
         * 5. Call Etherisc and create Policy
         */
        throw new NotImplementedException();
    }

    //create loctaion on etherisc here.
    private LocationResponse createLocation()
    {
        //TODO implement.
        return new LocationResponse();
    }
    
    //create configuration on etherisc here.
    private ConfigResponse createConfiguration()
    {
        //TODO implement.
        return new ConfigResponse();
    }

    //create person on etherisc here.
    private PersonResponse createPerson()
    {
        //TODO implement.
        return new PersonResponse() ;
    }
}