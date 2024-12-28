namespace eSusInsurers.Models.SeasonCutOffDate;

public class SeasonCutOffDatesFilterOptions
{
    /// <summary>
    ///     Filter based on region of season cutoff dates.
    /// </summary>
    public string? Region { get; set; }

    /// <summary>
    ///     Filter based on crop of season cutoff dates.
    /// </summary>
    public string? Crop { get; set; }

    /// <summary>
    ///     Filter based on crop category of season cutoff dates.
    /// </summary>
    public string? CropCategory { get; set; }

    /// <summary>
    ///     Filter based on year of season cutoff dates.
    /// </summary>
    public string? Year { get; set; }

    /// <summary>
    ///     Filter based on season of  season cutoff dates.
    /// </summary>
    public string? Season { get; set; }

    /// <summary>
    ///     Filter based on active of season cutoff dates.
    /// </summary>
    public bool? IsActive { get; set; }
}