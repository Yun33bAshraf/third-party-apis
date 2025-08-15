﻿namespace ThirdPartyAPIs.Domain.Entities;
public class LoginAttempts :BaseAuditableEntity
{
    public int UserId { get; set; }
    public int RejectionType { get; set; }
    public int LoginType { get; set; }
    public DateTime AttemptDate { get; set; }
    public bool IsSuccess { get; set; }
    public string IpAddress { get; set; } = null!;
    public string ClientInformation { get; set; } = null!;
    public bool TwoFacVerified { get; set; }
    public string? DeviceRegistration { get; set; }

    public virtual User? User { get; set; }
}
