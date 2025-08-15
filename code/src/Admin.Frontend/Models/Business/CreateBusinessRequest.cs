using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IApply.Frontend.Models.Business;


public class CreateBusinessRequest
{
    [Display(Name = "Business ID")]
    public int Id { get; set; }

    [Display(Name = "Business Name")]
    public string BusinessName { get; set; } = string.Empty;

    [Display(Name = "Business Location Name")]
    public string BusinessLocationName { get; set; } = string.Empty;

    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Display(Name = "Landmark")]
    public string LandMark { get; set; } = string.Empty;

    [Display(Name = "Country")]
    public string Country { get; set; } = string.Empty;

    [Display(Name = "State")]
    public string State { get; set; } = string.Empty;

    [Display(Name = "City")]
    public string City { get; set; } = string.Empty;

    [Display(Name = "Zip Code")]
    public string ZipCode { get; set; } = string.Empty;

    [Display(Name = "Currency ID")]
    public int CurrencyId { get; set; }

    [Display(Name = "Default Profit %")]
    public double DefaultProfitPercent { get; set; }

    [Display(Name = "Default Shipping %")]
    public double DefaultShippingPercent { get; set; }

    [Display(Name = "Time Zone")]
    public string TimeZone { get; set; } = string.Empty;

    [Display(Name = "Financial Year Start Month")]
    public int FyStartMonth { get; set; }

    [Display(Name = "Enable Product Expiry")]
    public bool EnableProductExpiry { get; set; }

    [Display(Name = "Username")]
    public string UserName { get; set; } = string.Empty;

    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Password")]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Keyboard Shortcuts")]
    public KeyboardShortcuts? KeyboardShortcuts { get; set; }

    [Display(Name = "POS Settings")]
    public PosSettings? PosSettings { get; set; }

    [Display(Name = "Weighing Scale Setting")]
    public WeighingScaleSetting? WeighingScaleSetting { get; set; }

    [Display(Name = "Reference No Prefixes")]
    public RefNoPrefixes? RefNoPrefixes { get; set; }

    [Display(Name = "Enable Brand")]
    public bool EnableBrand { get; set; }

    [Display(Name = "Enable Category")]
    public bool EnableCategory { get; set; }

    [Display(Name = "Enable Subcategory")]
    public bool EnableSubCategory { get; set; }

    [Display(Name = "Enable Price Tax")]
    public bool EnablePriceTax { get; set; }

    [Display(Name = "Enable Purchase Status")]
    public bool EnablePurchaseStatus { get; set; }

    [Display(Name = "Comment")]
    public string Comment { get; set; } = string.Empty;


    [Display(Name = "Active")]
    public bool IsActive { get; set; }
}

public class KeyboardShortcuts
{
    [JsonPropertyName("expressCheckout")]
    public string? ExpressCheckout { get; set; }

    [JsonPropertyName("payNCkeckout")]
    public string? PayAndCheckout { get; set; }

    [JsonPropertyName("draft")]
    public string? Draft { get; set; }

    [JsonPropertyName("cancel")]
    public string? Cancel { get; set; }

    [JsonPropertyName("quotation")]
    public string? Quotation { get; set; }

    [JsonPropertyName("checkoutMultiPay")]
    public string? CheckoutMultiPay { get; set; }

    [JsonPropertyName("recentTransactions")]
    public string? RecentTransactions { get; set; }

    [JsonPropertyName("recentProductQuantity")]
    public string? RecentProductQuantity { get; set; }

    [JsonPropertyName("weighingScale")]
    public string? WeighingScale { get; set; }

    [JsonPropertyName("editDiscount")]
    public string? EditDiscount { get; set; }

    [JsonPropertyName("editOrderTax")]
    public string? EditOrderTax { get; set; }

    [JsonPropertyName("addPaymentRow")]
    public string? AddPaymentRow { get; set; }

    [JsonPropertyName("finalizePayment")]
    public string? FinalizePayment { get; set; }

    [JsonPropertyName("addNewProduct")]
    public string? AddNewProduct { get; set; }
}

public class PosSettings
{
    [JsonPropertyName("cmmsnCalculationType")]
    public string? CmmsnCalculationType { get; set; }

    [JsonPropertyName("amountRoundingMethod")]
    public string? AmountRoundingMethod { get; set; }

    [JsonPropertyName("allowOverselling")]
    public string? AllowOverselling { get; set; }

    [JsonPropertyName("razorPayKeyId")]
    public string? RazorPayKeyId { get; set; }

    [JsonPropertyName("razorPayKeySecret")]
    public string? RazorPayKeySecret { get; set; }

    [JsonPropertyName("stripePublicKey")]
    public string? StripePublicKey { get; set; }

    [JsonPropertyName("stripeSecretKey")]
    public string? StripeSecretKey { get; set; }

    [JsonPropertyName("cashDenominations")]
    public string? CashDenominations { get; set; }

    [JsonPropertyName("disablePayCheckout")]
    public bool DisablePayCheckout { get; set; }

    [JsonPropertyName("disableDraft")]
    public bool DisableDraft { get; set; }

    [JsonPropertyName("disableExpressCheckout")]
    public bool DisableExpressCheckout { get; set; }

    [JsonPropertyName("hideProductSuggestion")]
    public bool HideProductSuggestion { get; set; }

    [JsonPropertyName("hideRecentTrans")]
    public bool HideRecentTransactions { get; set; }

    [JsonPropertyName("disableDiscount")]
    public bool DisableDiscount { get; set; }

    [JsonPropertyName("disableOrderTax")]
    public bool DisableOrderTax { get; set; }

    [JsonPropertyName("isPosSubtotalEditable")]
    public bool IsPosSubtotalEditable { get; set; }
}

public class WeighingScaleSetting
{
    [JsonPropertyName("labelPrefix")]
    public string? LabelPrefix { get; set; } = string.Empty;

    [JsonPropertyName("productSkuLength")]
    public string? ProductSkuLength { get; set; } = string.Empty;

    [JsonPropertyName("qtyLength")]
    public string? QtyLength { get; set; } = string.Empty;

    [JsonPropertyName("qtyLengthDecimal")]
    public string? QtyLengthDecimal { get; set; } = string.Empty;
}

public class RefNoPrefixes
{
    [JsonPropertyName("purchase")]
    public string Purchase { get; set; } = string.Empty;

    [JsonPropertyName("purchaseReturn")]
    public string? PurchaseReturn { get; set; } = string.Empty;

    [JsonPropertyName("purchaseOrder")]
    public string? PurchaseOrder { get; set; } = string.Empty;

    [JsonPropertyName("stockTransfer")]
    public string? StockTransfer { get; set; } = string.Empty;

    [JsonPropertyName("stockAdjustment")]
    public string? StockAdjustment { get; set; } = string.Empty;

    [JsonPropertyName("saleReturn")]
    public string? SaleReturn { get; set; } = string.Empty;

    [JsonPropertyName("expense")]
    public string? Expense { get; set; } = string.Empty;

    [JsonPropertyName("contacts")]
    public string? Contacts { get; set; } = string.Empty;

    [JsonPropertyName("purchasePayment")]
    public string? PurchasePayment { get; set; } = string.Empty;

    [JsonPropertyName("salePayment")]
    public string? SalePayment { get; set; } = string.Empty;

    [JsonPropertyName("expensePayment")]
    public string? ExpensePayment { get; set; } = string.Empty;

    [JsonPropertyName("businessLocation")]
    public string? BusinessLocation { get; set; } = string.Empty;

    [JsonPropertyName("draft")]
    public string? Draft { get; set; } = string.Empty;

    [JsonPropertyName("salesOrder")]
    public string? SalesOrder { get; set; } = string.Empty;

    [JsonPropertyName("transfer")]
    public string? Transfer { get; set; } = string.Empty;
}
