using IApply.Frontend.Models.Auth;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OpenProfileAPI.Frontend.Models.System.DeviceSetting;

public class DeviceSetting
{
    [Required]
    [Range(300, int.MaxValue, ErrorMessage = "Should be greater or equal to 300 Seconds")]
    public int SettingsUpdateInterval { get; set; }

    [Required]
    [Range(120, int.MaxValue, ErrorMessage = "Should be greater or equal to 120 Seconds")]
    public int LocationUpdateInterval { get; set; }

    [Required]
    [Range(60, int.MaxValue, ErrorMessage = "Should be greater or equal to 60 Seconds")]
    public int RideSearchTimeout { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Should be greater or equal to 1 KM")]
    public int MinSearchRadiusInKm { get; set; }

    [Required]
    [SearchRadiusValidation]
    [Range(0, 30, ErrorMessage = "Should be less than or equal to 30 KM")]
    public int MaxSearchRadiusInKm { get; set; }

    [Required]
    [Range(60, int.MaxValue, ErrorMessage = "Should be greater or equal to 60 Seconds")]
    public int RideAcceptTimeout { get; set; }

    [Required]
    [Range(60, int.MaxValue, ErrorMessage = "Should be greater or equal to 60 Seconds")]
    public int CustomerArrivalTimeout { get; set; }

    [Required]
    [Range(60, int.MaxValue, ErrorMessage = "Should be greater or equal to 60 Seconds")]
    public int DriverArrivalTimeout { get; set; }

}
public class SearchRadiusValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var deviceSettingRequest = (DeviceSetting)validationContext.ObjectInstance;


        if (value != null && (int)value <= deviceSettingRequest.MinSearchRadiusInKm)
        {
            var memberNames = new[] { nameof(deviceSettingRequest.MaxSearchRadiusInKm) };
            return new ValidationResult(
                "Max Search Radius must be greater than Min Search Radius.",
                memberNames
            );
        }


        return ValidationResult.Success;
    }
}

