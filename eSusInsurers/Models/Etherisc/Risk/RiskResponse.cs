namespace eSusInsurers.Models.Etherisc;

public class RiskResponse
{
    public String id { get; set; }
    public Boolean isValid { get; set; }
    public String configId { get; set; }
    public long createdAt { get; set; }
    public String crop { get; set; }
    public String locationId { get; set; }
    public double deductible { get; set; }
    public long updatedAt { get; set; }
}