using System.ComponentModel.DataAnnotations;

namespace eSusInsurers.Models.enums
{
    public enum SearchOperationEnum
    {
        [Display(Name = "=")]
        Equal,
        Begins,
        [Display(Name = "Contains")]
        Contains,
        [Display(Name = "LContains")]
        LContains,
        Ends,
        [Display(Name = ">")]
        GreaterThan,
        [Display(Name = "<")]
        LessThan,
        All,
        Any,
        [Display(Name = "<>")]
        NotEqual,
        [Display(Name = "<=")]
        LessEqualThan,
        [Display(Name = ">=")]
        GreaterEqualThan
    }
}
