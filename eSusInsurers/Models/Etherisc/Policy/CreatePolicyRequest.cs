namespace eSusInsurers.Models.Etherisc.Policy;

public class CreatePolicyRequest
{
    public string personId { get; set; }
    public string riskId { get; set; }
    public string externalId { get; set; }
    public string subscriptionDate { get; set; }
    public double sumInsuredAmount { get; set; }
    public double premiumAmount { get; set; }
    public string onchainId { get; set; }
    
}