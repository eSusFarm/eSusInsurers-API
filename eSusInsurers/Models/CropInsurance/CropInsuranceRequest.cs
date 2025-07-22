namespace eSusInsurers.Models.CropInsurance;

public class CropInsuranceRequest
{
    private int farmerId { get; set; }
    private int farmerCropId { get; set; }
    private String cropName { get; set; }
    private double lattitude { get; set; }
    private double longitude { get; set; }
    private int insurancePolicyId { get; set; }
    private int insuranceRiskId { get; set; }
    private String status { get; set; }
    private Boolean isActive { get; set; }
    
}