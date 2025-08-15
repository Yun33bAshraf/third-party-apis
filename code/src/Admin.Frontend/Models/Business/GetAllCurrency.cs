using IApply.Frontend.Common.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace IApply.Frontend.Models.Business;


public class GetAllCurrency
{
    [Display(Name = "ID")]
    public int Id { get; set; }

    [Display(Name = "Country")]
    public string Country { get; set; } = string.Empty;

    [Display(Name = "Currency Name")]
    public string CurrencyName { get; set; } = string.Empty;

    [Display(Name = "Code")]
    public string Code { get; set; } = string.Empty;

    [Display(Name = "Symbol")]
    public string Symbol { get; set; } = string.Empty;

    [Display(Name = "Thousand Separator")]
    public string ThousandSeparator { get; set; } = string.Empty;

    [Display(Name = "Decimal Separator")]
    public string DecimalSeparator { get; set; } = string.Empty;
}

