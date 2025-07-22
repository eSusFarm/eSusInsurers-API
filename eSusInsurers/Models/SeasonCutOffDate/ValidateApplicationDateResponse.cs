namespace eSusInsurers.Models.SeasonCutOffDate
{
    public class ValidateApplicationDateResponse
    {
        public Boolean IsSeasonClosed { get; set; } 
        public String Message { get; set; } 
        public String StatusCode { get; set; } 
        public DateOnly SeasonCutOffDate { get; set; }
    }
}

