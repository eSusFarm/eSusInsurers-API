namespace eSusInsurers.Models.Common
{
    /// <summary>
    /// Data sorting options.
    /// </summary>
    public class SortingOptions
    {
        /// <summary>
        /// The data property to be sorted on.
        /// </summary>
        /// <example>CreatedDate</example>
        public string? SortOn { get; set; }

        /// <summary>
        /// The sorting direction that should be applied to the sorted on property.
        /// </summary>
        /// <example>Ascending</example>
        public string? SortDirection { get; set; } = "Ascending";
    }
}
