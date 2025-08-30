namespace OpenProfileAPI.Domain.Enums;
public enum ApplicationRights
{
    [RightAttribute(1, roleIds: [1, 2])]
    ViewProfile = 1,

    [RightAttribute(2, roleIds: [1, 2])]
    EditProfile = 2,

    [RightAttribute(3, roleIds: [1])]
    ViewCustomers = 3,

    [RightAttribute(4, roleIds: [1])]
    ViewCustomerById = 4,

    [RightAttribute(5, roleIds: [1])]
    AddAdmins = 5,

    [RightAttribute(6, roleIds: [1])]
    EditAdmins = 6,

    [RightAttribute(7, roleIds: [1])]
    ActivateAdmins = 7,

    [RightAttribute(8, roleIds: [1])]
    DeactivateAdmins = 8,

    [RightAttribute(9, roleIds: [1])]
    ActivateCustomers = 9,

    [RightAttribute(10, roleIds: [1])]
    DeactivateCustomers = 10,
}

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public sealed class RightAttribute : Attribute
{
    public int Id { get; }
    public int[] RoleIds { get; }

    public RightAttribute(int id, int[] roleIds)
    {
        Id = id;
        RoleIds = roleIds;
    }
}

public static class RightEnumAttributeExtensions
{
    public static (int Id, string Name, int Value, int[] RoleIds) ToRightAttribute(this ApplicationRights right)
    {
        var fieldInfo = right.GetType().GetField(right.ToString());
        var attribute = fieldInfo?.GetCustomAttributes(typeof(RightAttribute), false)
                                 .FirstOrDefault() as RightAttribute;

        return attribute != null
            ? (attribute.Id, right.ToReadableString(), (int)right, attribute.RoleIds)
            : (0, string.Empty, 0, Array.Empty<int>());
    }
}

