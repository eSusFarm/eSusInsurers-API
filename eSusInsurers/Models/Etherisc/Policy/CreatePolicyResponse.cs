namespace eSusInsurers.Models.Etherisc.Policy;

public class CreatePolicyResponse
{
    public string id { get; set; }
    public string personId { get; set; }
    public string riskId { get; set; }
    public string externalId { get; set; }
    public string subscriptionDate { get; set; }
    public double subscriptionAmount { get; set; }
    public double sumInsuredAmount { get; set; }
    public double premiumAmount { get; set; }
    public string onChainId { get; set; }
}