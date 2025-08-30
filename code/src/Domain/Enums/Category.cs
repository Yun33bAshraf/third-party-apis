using System.ComponentModel;

namespace OpenProfileAPI.Domain.Enums;

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

public enum Category
{
    // Country
    [Category((int)Country.Pakistan, (int)EntityType.Country, nameof(Country.Pakistan))]
    Pakistan = 1,

    // State/Province
    [Category((int)StateProvince.FederalCapital, (int)EntityType.StateProvince, nameof(StateProvince.FederalCapital), (int)Country.Pakistan)]
    FederalCapital = 2,

    [Category((int)StateProvince.Punjab, (int)EntityType.StateProvince, nameof(StateProvince.Punjab), (int)Country.Pakistan)]
    Punjab = 3,

    // City
    [Category((int)City.Islamabad, (int)EntityType.City, nameof(City.Islamabad), (int)StateProvince.FederalCapital)]
    Islamabad = 4,

    [Category((int)City.Rawalpindi, (int)EntityType.City, nameof(City.Rawalpindi), (int)StateProvince.Punjab)]
    Rawalpindi = 5,

    [Category((int)City.Lahore, (int)EntityType.City, nameof(City.Lahore), (int)StateProvince.Punjab)]
    Lahore = 6,

    // Marital Status
    [Category((int)MaritalStatus.Single, (int)EntityType.MaritalStatus, nameof(MaritalStatus.Single))]
    Single = 7,

    [Category((int)MaritalStatus.Married, (int)EntityType.MaritalStatus, nameof(MaritalStatus.Married))]
    Married = 8,

    // Gender
    [Category((int)Gender.Male, (int)EntityType.Gender, nameof(Gender.Male))]
    Male = 9,

    [Category((int)Gender.Female, (int)EntityType.Gender, nameof(Gender.Female))]
    Female = 10,

    // Skills
    [Category((int)Skill.Coding, (int)EntityType.Skill, nameof(Skill.Coding))]
    Coding = 11,

    [Category((int)Skill.Debugging, (int)EntityType.Skill, nameof(Skill.Debugging))]
    Debugging = 12,

    [Category((int)Skill.Testing, (int)EntityType.Skill, nameof(Skill.Testing))]
    Testing = 13,

    [Category((int)Skill.Scripting, (int)EntityType.Skill, nameof(Skill.Scripting))]
    Scripting = 14,

    [Category((int)Skill.Integration, (int)EntityType.Skill, nameof(Skill.Integration))]
    Integration = 15,
}


public sealed class CategoryAttribute : Attribute
{
    public int Id { get; }
    public int EntityTypeId { get; }
    public string? Description { get; }
    public int ParentCategoryId { get; }

    public CategoryAttribute(int id, int entityTypeId = 0, string? description = null, int parentCategoryId = 0)
    {
        Id = id;
        EntityTypeId = entityTypeId;
        Description = description;
        ParentCategoryId = parentCategoryId;
    }
}

public static class CategoryExtensions
{
    public static (int Id, string Name, int Value, int EntityTypeId, int ParentCategoryId, string? Description)
        ToCategoryAttribute(this Category category)
    {
        var fieldInfo = category.GetType().GetField(category.ToString());
        var attribute = fieldInfo?.GetCustomAttributes(typeof(CategoryAttribute), false)
                                 .FirstOrDefault() as CategoryAttribute;

        if (attribute == null)
            return (0, string.Empty, 0, 0, 0, null);

        string toReadableValue = category.ToReadableString();

        return (
            attribute.Id,
            toReadableValue,
            (int)category,
            attribute.EntityTypeId,
            attribute.ParentCategoryId,
            toReadableValue
        );
    }
}
