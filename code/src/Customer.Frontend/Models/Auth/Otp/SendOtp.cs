namespace IApply.Frontend.Models.Auth.Otp;

public class SendOtpRequest
{
    public int? TwoFactorMethod { get; set; }
}

public class SendOtpResponse
{
    public int ErrorCode { get; set; }
    public string? ErrorHint { get; set; }
    public bool IsSuccess { get; set; }
}
