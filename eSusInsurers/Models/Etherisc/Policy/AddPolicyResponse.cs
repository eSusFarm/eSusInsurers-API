namespace eSusInsurers.Models.Etherisc.Policy;

public class AddPolicyResponse
{
    public string message { get; set; }
    public int statusCode { get; set; }
    public String policyId { get; set; }
    public String riskId { get; set; }
    public int cropInsuranceId { get; set; }
}