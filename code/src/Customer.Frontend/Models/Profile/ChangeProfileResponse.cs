using System;

namespace IApply.Frontend.Models.Profile;

public class ChangeProfileResponse
{
    public int ErrorCode { get; set; }
    public string? ErrorHint { get; set; }
    public bool IsSuccess { get; set; }
}
