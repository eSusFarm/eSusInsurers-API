using System.Runtime.InteropServices.JavaScript;

namespace eSusInsurers.Models.Etherisc.Config;

/// <summary>
/// 
/// </summary>
public class ConfigResponse
{
    private String id    { get; set; }
    private Boolean isValid    { get; set; }
    private String name  { get; set; }
    private String year  { get; set; }
    private String startOfSeason  { get; set; }
    private String endOfSeason  { get; set; }
    private String seasonDays  { get; set; }
    private String franchise  { get; set; }
    private String updatedAt  { get; set; }
}