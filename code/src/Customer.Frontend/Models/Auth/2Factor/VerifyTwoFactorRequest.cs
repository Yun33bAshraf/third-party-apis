using System;
using System.ComponentModel.DataAnnotations;

namespace IApply.Frontend.Models.Auth._2Factor;

public class VerifyTwoFactorRequest
{
[Display(Name = "Verification Code")]
 public string? Code { get; set; }
}
