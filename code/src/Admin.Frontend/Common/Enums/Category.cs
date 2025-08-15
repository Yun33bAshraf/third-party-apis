using System.ComponentModel;

namespace IApply.Frontend.Common.Enums;

public enum LeaveType
{
    [Description("Annual")]
    Annual = 1,
    [Description("Sick")]
    Sick,
    [Description("Casual")]
    Casual,
    [Description("Maternity")]
    Maternity,
    [Description("Paternity")]
    Paternity,
    [Description("Unpaid")]
    Unpaid,
    [Description("Other")]
    Other,
    [Description("Half Leave")]
    HalfLeave
}

public enum LeaveStatus
{
    [Description("Approved")]
    Approved = 1,
    [Description("Pending")]
    Pending,
    [Description("Rejected")]
    Rejected
}

public enum AssetCategory
{
    [Description("Electronics")]
    Electronics = 3,
    [Description("Machinery")]
    Machinery = 4,
    [Description("Software")]
    Software = 5
}

public enum AssetSubCategory
{
    [Description("Laptop")]
    Laptop = 6,
    [Description("Mobile Phone")]
    MobilePhone = 7,
    [Description("Tablet")]
    Tablet = 8,
    [Description("Generator")]
    Generator = 9,
    [Description("Excavator")]
    Excavator = 10,
    [Description("Operating System")]
    OperatingSystem = 11,
    [Description("Application Software")]
    ApplicationSoftware = 12
}

public enum AssetStatus
{
    [Description("Operational")]
    Operational = 1,
    [Description("Maintenance")]
    Maintenance,
    [Description("Retired")]
    Retired
}