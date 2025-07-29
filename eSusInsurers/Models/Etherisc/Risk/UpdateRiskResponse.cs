namespace eSusInsurers.Models.Etherisc;

public class UpdateRiskResponse
{
    public string configId { get; set; }
    public long createdAt { get; set; }
    public string crop { get; set; }
    public int deductible { get; set; }
    public double draughtLoss { get; set; }
    public double excessRainfallLoss { get; set; }
    public double finalPayout { get; set; }
    public bool  isValid { get; set; }
    public string locationId { get; set; }
    public double payout { get; set; }
    public string startOfSeason { get; set; }
    public double totalLoss { get; set; }
    public long updatedAt { get; set; }
}