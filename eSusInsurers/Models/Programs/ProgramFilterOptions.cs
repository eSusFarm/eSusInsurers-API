namespace eSusInsurers.Models.Programs
{
    public class ProgramFilterOptions
    {
        /// <summary>
        /// Filter based on active programs.
        /// </summary>
        public bool? IsActive { get; set; }

        public string? ProgramName { get; set; }

        public string? RegionName { get; set; }

        public string? DistrictName { get; set; }

        public string? SubCountyName { get; set; }

        public string? ParishName { get; set; }
    }
}
