using System;

namespace IApply.Frontend.Models.Auth._2Factor;

public class ConfigureAuthenticatorResponse
{
 public string? QrCodeUrl { get; set; }
 public string? QrCode { get; set; }
 public string? Message { get; set; }
 public bool IsSuccess { get; set; } = false;
 public int ErrorCode { get; set; }


}
