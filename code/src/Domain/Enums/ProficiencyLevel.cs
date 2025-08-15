using System.ComponentModel;

namespace ThirdPartyAPIs.Domain.Enums;
public enum ProficiencyLevel
{
    [Description("Beginner")]
    Beginner = 1,
    [Description("Intermediate")]
    Intermediate = 2,
    [Description("Expert")]
    Expert = 3,
}
