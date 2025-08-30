using System.ComponentModel;

namespace OpenProfileAPI.Domain.Enums
{
    public enum RoleType
    {
        [Description("Administration")]
        Administration = 1,

        [Description("Customer")]
        Customer = 2
    }

    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class RoleAttribute : Attribute
    {
        public int Id { get; }
        public string? Description { get; }

        public RoleAttribute(int id, string? description = null)
        {
            Id = id;
            Description = description;
        }
    }

    public static class RoleEnumAttributeExtensions
    {
        public static (int Id, string Name, int Value, string? Description) ToRoleAttribute(this RoleType role)
        {
            var type = role.GetType();
            var fieldInfo = type.GetField(role.ToString());
            var descriptionAttribute = fieldInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                                                .FirstOrDefault() as DescriptionAttribute;

            string displayName = descriptionAttribute?.Description ?? role.ToString();

            return (
                (int)role,
                displayName,
                (int)role,
                displayName
            );
        }
    }
}
