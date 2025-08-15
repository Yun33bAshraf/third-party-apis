using System.ComponentModel;

namespace ThirdPartyAPIs.Domain.Enums;

public enum EntityType
{
    [Description("Country")]
    Country = 1,
    [Description("State/Province")]
    StateProvince = 2,
    [Description("City")] 
    City = 3,
    [Description("Marital Status")] 
    MaritalStatus = 4,
    [Description("Gender")] 
    Gender = 5,
    [Description("Skills")]
    Skill = 6,
}
