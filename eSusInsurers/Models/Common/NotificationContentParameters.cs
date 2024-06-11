namespace eSusInsurers.Models.Common
{
    public class NotificationContentParameters
    {
        public string Index0 { get; set; }
        public string Index1 { get; set; }
        public string Index2 { get; set; }
        public string Index3 { get; set; }
        public string URL { get; set; } = "https://esusinsurernonprodapp.azurewebsites.net/";
        public string? UserName { get; set; }
        public string? AppName { get; set; } = "eSusInsurers Application";
        public string? EmailId { get; set; }
        public string? Password { get; set; }
        public string CompanyName { get; set; } = "eSusFarm";
    }
}
