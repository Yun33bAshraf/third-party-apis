namespace IApply.Frontend.Models.Auth.Otp;

public class VerifyOtpRequest
{
    public int? TwoFactorMethod { get; set; }
    public string? Code { get; set; }
}
