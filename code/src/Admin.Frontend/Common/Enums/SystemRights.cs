namespace IApply.Frontend.Common.Enums
{
    public enum SystemRights
    {
        [Right("DAC8E98B-E757-401D-9456-B9F2AFE08266", "Read all users")]
        ReadAllUsers = 9,

        [Right("F395E6C4-F03C-4670-B127-D2A6AC91A68F", "Create users in backend")]
        CreateUsersInBackend = 10,

        [Right("63B1B655-EC5D-4AD5-AF06-1B99F9F4DC7E", "View system rights")]
        ReadRights = 11,

        [Right("DD24083B-EC64-4EAF-909B-AE9CE7A49D97", "Read roles")]
        ReadRoles = 12,

        [Right("8C720D6D-380C-4D3E-AA86-A37DCA3E0AC9", "Create roles")]
        CreateRoles = 13,

        [Right("71C3B1E6-089A-48C5-9418-6B7B58750AE0", "Update roles")]
        UpdateRoles = 14,

        [Right("C8DE7BE3-02E9-4D22-8A08-80D8A6C3B3D3", "Delete roles")]
        DeleteRoles = 15,

        [Right("5FD5EDA1-D464-4314-B549-A8848A4DB0E7", "Assign rights to roles")]
        AssignRightsToRoles = 16,

        [Right("93B51FA0-C20F-46A2-A192-915BF825F0D3", "Unassign rights to roles")]
        UnassignRightsToRoles = 17,

        [Right("A2138FC3-2A3D-4750-8FF0-10D4998D9A49", "Assign right to users")]
        AssignRightsToUsers = 18,

        [Right("2F702F32-6B6E-48BE-985F-50016E19B418", "Unassign right to users")]
        UnassignRightsToUsers = 19,

        [Right("1BD46A54-3D83-4C4D-9221-AA862A067F3F", "Read user rights")]
        ReadUserRights = 20,

        [Right("92ED7B10-EC1D-4836-ACED-41A5DC821DB4", "Read role rights")]
        ReadRoleRights = 21,

        [Right("A6304909-437F-41E7-854A-5F8FC56AD6FB", "Read user roles")]
        ReadUserRoles = 22,

        [Right("D9E33A14-9B84-4318-B498-E7B6DF583F55", "Assign user roles")]
        AssignUserRoles = 23,

        [Right("2F6C873E-FD69-4220-AE6A-5788110C13F0", "Unassign user roles")]
        UnassignUserRoles = 24,

        [Right("33100952-CB32-4225-AB13-18BC61E8B885", "View auth policies")]
        ViewAuthPolicies = 30,

        [Right("A2F52E25-DE78-4AEE-AB4D-379D15CFE8B2", "Modify auth policy")]
        ModifyAuthPolicy = 31,

        [Right("1657951E-C760-4FBB-BC09-22D5E8C2FD46", "Read user files")]
        ReadUserFiles = 25,

        [Right("3285D81D-CEE1-4B0E-914B-683E60A3AD34", "Update user file status")]
        UpdateUserFileStatus = 26,

        [Right("0E0E925F-387E-4EC2-AD91-443E5BD8D38A", "Update user status")]
        UpdateUserStatus = 27,

        [Right("659C2ED9-1042-4645-80F0-9E11C575C8F2", "View ride plan")]
        ViewRidePlan = 28,

        [Right("A5ECCE21-94B7-41A3-BD3E-B469AB1BFB85", "Modify ride plan")]
        ModifyRidePlan = 29,

        [Right("7727FEB5-0D22-46AE-9052-C437CEED0CC2", "View vehicle type")]
        ViewVehicleType = 32,

        [Right("263E1C66-0F81-467A-8952-2913C5D0F799", "Modify vehicle type")]
        ModifyVehicleType = 33,

        [Right("0AFA9663-BB65-4A8F-BD47-B18031FEB3C4", "View vehicle catalog")]
        ViewVehicleCatalog = 34,

        [Right("A58CF786-E471-4A7A-9098-32DA281E242F", "Modify vehicle catlog")]
        ModifyVehicleCatalog = 35,

        [Right("1A50F5F8-C081-47B2-BB98-A78262C8F3CA", "View cities")]
        ViewCities = 36,

        [Right("41B39FC4-BDCB-4106-9921-03DD8EB06B65", "Modify cities")]
        ModifyCities = 37,

        [Right("86BA0BAC-D2FE-4181-A727-7F500C4EB970", "View required documents")]
        ViewRequiredDocuments = 38,

        [Right("2F038132-7010-44D0-9BD5-3D68583DA5E6", "Modify required documents")]
        ModifyRequiredDocuments = 39,

        [Right("D306243D-D7DB-459F-B235-DF30925F8C98", "Modify device settings")]
        ModifyDeviceSettings = 40,

    }


    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class RightAttribute : Attribute
    {
        public Guid Id { get; }
        public string Description { get; }

        public RightAttribute(string id, string description)
        {
            Id = Guid.Parse(id);
            Description = description;
        }
    }

    public static class EnumExtensions
    {
        public static (Guid Id, string Name, string Description, int value) ToRightAttribute(this SystemRights right)
        {
            var fieldInfo = right.GetType()
                .GetField(right.ToString());

            var attribute = fieldInfo?.GetCustomAttributes(typeof(RightAttribute), false)
                .FirstOrDefault() as RightAttribute;

            return attribute != null ? (attribute.Id, right.ToString(), attribute.Description, (int)right) : (Guid.Empty, string.Empty, string.Empty, 0);
        }
    }
}
