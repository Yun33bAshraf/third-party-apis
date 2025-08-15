using IApply.Frontend.Common.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace IApply.Frontend.Models.Business;


public class BusinessListing
{
    [Display(Name = "ID")]
    public int Id { get; set; }

    [Display(Name = "Name")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Currency")]
    public int CurrencyId { get; set; }

    [Display(Name = "Start Date")]
    public DateTime StartDate { get; set; }

    [IgnoreInTable]
    [Display(Name = "Tax Number 1")]
    public string? TaxNumber1 { get; set; }

    [IgnoreInTable]
    [Display(Name = "Tax Label 1")]
    public string? TaxLabel1 { get; set; }

    [IgnoreInTable]
    [Display(Name = "Tax Number 2")]
    public string? TaxNumber2 { get; set; }

    [IgnoreInTable]
    [Display(Name = "Tax Label 2")]
    public string? TaxLabel2 { get; set; }

    [IgnoreInTable]
    [Display(Name = "Code Label 1")]
    public string? CodeLabel1 { get; set; }

    [IgnoreInTable]
    [Display(Name = "Code 1")]
    public string? Code1 { get; set; }

    [IgnoreInTable]
    [Display(Name = "Code Label 2")]
    public string? CodeLabel2 { get; set; }

    [IgnoreInTable]
    [Display(Name = "Code 2")]
    public string? Code2 { get; set; }

    [IgnoreInTable]
    [Display(Name = "Default Profit %")]
    public double DefaultProfitPercent { get; set; }

    [IgnoreInTable]
    [Display(Name = "Default Shipping %")]
    public double DefaultShippingPercent { get; set; }

    [IgnoreInTable]
    [Display(Name = "Owner ID")]
    public int OwnerId { get; set; }

    
    [Display(Name = "Time Zone")]
    public string TimeZone { get; set; } = string.Empty;


    [Display(Name = "Financial Year Start Month")]
    public int FyStartMonth { get; set; }

    [IgnoreInTable]
    [Display(Name = "Logo")]
    public string? Logo { get; set; }

    [IgnoreInTable]
    [Display(Name = "SKU Prefix")]
    public string? SkuPrefix { get; set; }

    [IgnoreInTable]
    [Display(Name = "Enable Product Expiry")]
    public bool EnableProductExpiry { get; set; }

    [IgnoreInTable]
    [Display(Name = "Expiry Type")]
    public string ExpiryType { get; set; } = string.Empty;

    [IgnoreInTable]
    [Display(Name = "On Product Expiry")]
    public string OnProductExpiry { get; set; } = string.Empty;

    [IgnoreInTable]
    [Display(Name = "Stock Expiry Alert (Days)")]
    public int StockExpiryAlertDays { get; set; }

    [IgnoreInTable]
    [Display(Name = "Keyboard Shortcuts (JSON)")]
    public string? KeyboardShortcuts { get; set; }

    [IgnoreInTable]
    [Display(Name = "POS Settings (JSON)")]
    public string? PosSettings { get; set; }

    [IgnoreInTable]
    [Display(Name = "Weighing Scale Setting (JSON)")]
    public string? WeighingScaleSetting { get; set; }

    [IgnoreInTable]
    [Display(Name = "Manufacturing Settings")]
    public string? ManufacturingSettings { get; set; }

    [IgnoreInTable]
    [Display(Name = "Enable Brand")]
    public bool EnableBrand { get; set; }

    [IgnoreInTable]
    [Display(Name = "Enable Category")]
    public bool EnableCategory { get; set; }

    [IgnoreInTable]
    [Display(Name = "Enable Subcategory")]
    public bool EnableSubCategory { get; set; }

    [IgnoreInTable]
    [Display(Name = "Enable Price Tax")]
    public bool EnablePriceTax { get; set; }

    [IgnoreInTable]
    [Display(Name = "Enable Purchase Status")]
    public bool EnablePurchaseStatus { get; set; }

    [IgnoreInTable]
    [Display(Name = "Date Format")]
    public string DateFormat { get; set; } = string.Empty;

    [IgnoreInTable]
    [Display(Name = "Time Format")]
    public string TimeFormat { get; set; } = string.Empty;

    [IgnoreInTable]
    [Display(Name = "Reference No Prefixes (JSON)")]
    public string? RefNoPrefixes { get; set; }

    [Display(Name = "Active")]
    public bool IsActive { get; set; }

    [IgnoreInTable]
    [Display(Name = "Currency")]
    public GetAllCurrency? Currency { get; set; }

    [IgnoreInTable]
    [Display(Name = "Business Locations")]
    public List<object> BusinessLocations { get; set; } = new();

    [IgnoreInTable]
    [Display(Name = "Created By")]
    public int CreatedBy { get; set; }

    [IgnoreInTable]
    [Display(Name = "Created At")]
    public DateTime CreatedAt { get; set; }

    [IgnoreInTable]
    [Display(Name = "Modified By")]
    public int ModifiedBy { get; set; }

    [IgnoreInTable]
    [Display(Name = "Modified At")]
    public DateTime ModifiedAt { get; set; }

    [IgnoreInTable]
    [Display(Name = "Comment")]
    public string? Comment { get; set; }
}
