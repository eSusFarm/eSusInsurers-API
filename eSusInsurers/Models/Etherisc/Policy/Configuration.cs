namespace eSusInsurers.Models.Etherisc.Policy;

public class Configuration
{
    public Boolean isValid    { get; set; }
    public String name  { get; set; }
    public int year  { get; set; }
    public String startOfSeason  { get; set; }
    public String endOfSeason  { get; set; }
    public int seasonDays  { get; set; }
    public int franchise  { get; set; }
    public long updatedAt  { get; set; }
    public long createddAt  { get; set; }
}