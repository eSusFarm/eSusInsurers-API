namespace eSusInsurers.Models.Programs
{
    public class ProgramFilterOptions
    {
        /// <summary>
        /// Filter based on active programs.
        /// </summary>
        public bool? IsActive { get; set; }

        public string? ProgramName { get; set; }

        public int? RegionId { get; set; }

        public string? RegionName { get; set; }

        public int? DistrictId { get; set; }

        public string? DistrictName { get; set; }

        public int? SubcountyId { get; set; }

        public string? SubCountyName { get; set; }

        public int? ParishId { get; set; }

        public string? ParishName { get; set; }
    }
}
