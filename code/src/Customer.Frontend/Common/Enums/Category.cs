using System.ComponentModel;

namespace OpenProfileAPI.Frontend.Common.Enums;

// these are prequisite enums for the Category enum, all other must be added in category and entity type table.
public enum Country
{
    [Description("Pakistan")]
    Pakistan = 1,
}

public enum StateProvince
{
    [Description("Federal Capital")] FederalCapital = 2,
    [Description("Punjab")] Punjab = 3,
}

public enum City
{
    [Description("Islamabad")] Islamabad = 4,
    [Description("Rawalpindi")] Rawalpindi = 5,
    [Description("Lahore")] Lahore = 6,
}

public enum MaritalStatus
{
    [Description("Single")] Single = 7,
    [Description("Married")] Married = 8
}

public enum Gender
{
    [Description("Male")] Male = 9,
    [Description("Female")] Female = 10,
}

public enum Skill
{
    [Description("Coding")] Coding = 11,
    [Description("Debugging")] Debugging = 12,
    [Description("Testing")] Testing = 13,
    [Description("Scripting")] Scripting = 14,
    [Description("Integration")] Integration = 15,
}
