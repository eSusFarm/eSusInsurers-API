namespace eSusInsurers.Models.Etherisc;

public class UpdateRiskRequest
{
    public double draughtLoss  { get; set; }
    public double excessRainfallLoss  { get; set; }
    public double finalPayout  { get; set; }
    public string id  { get; set; }
    public double payout  { get; set; }
    public double totalLoss  { get; set; }
}