namespace eSusInsurers.Models.Etherisc.Config;

public class ConfigRequest
{
    public Boolean isValid    { get; set; }
    public String name  { get; set; }
    public long year  { get; set; }
    public String startOfSeason  { get; set; }
    public String endOfSeason  { get; set; }
    public int seasonDays  { get; set; }
    public double franchise  { get; set; }
    public long updatedAt  { get; set; }
    public long createdAt  { get; set; }
}