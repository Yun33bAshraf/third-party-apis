using System.ComponentModel;
using IApply.Frontend.Common.Enums;

public enum UserRights
{
    #region Assets

    /// <summary>Allows viewing asset list.</summary>
    [RightAttribute("1", "Allows viewing asset list.")]
    [Description("Assets View (Admin + HR)")]
    CanViewAssets = 1,

    [RightAttribute("2", "Allows viewing a single asset by ID.")]
    [Description("View Asset By Id (Admin + HR)")]
    CanViewAssetById = 2,

    [RightAttribute("3", "Allows adding new assets.")]
    [Description("Add Asset (Admin + HR)")]
    CanAddAssets = 3,

    [RightAttribute("4", "Allows editing existing assets.")]
    [Description("Edit Asset (Admin + HR)")]
    CanEditAssets = 4,

    #endregion

    #region Attendance

    [RightAttribute("5", "Allows viewing monthly attendance summary.")]
    [Description("Attendance View Monthly Summary (Admin + HR)")]
    CanViewMonthlyAttendanceSummary = 5,

    [RightAttribute("6", "Allows viewing today's attendance summary.")]
    [Description("Attendance View Today's Summary (Admin + HR + Employee)")]
    CanViewTodaysAttendanceSummary = 6,

    [RightAttribute("7", "Allows adding attendance records.")]
    [Description("Add Attendance (Admin + HR)")]
    CanAddAttendance = 7,

    [RightAttribute("8", "Allows editing attendance records.")]
    [Description("Edit Attendance (Admin + HR)")]
    CanEditAttendance = 8,

    #endregion

    #region Employees

    [RightAttribute("9", "Allows viewing employee list.")]
    [Description("Employee Listing (Admin + HR)")]
    CanViewEmployeeListing = 9,

    [RightAttribute("10", "Allows viewing an employee by ID.")]
    [Description("View Employee By Id (Admin + HR)")]
    CanViewEmployeeById = 10,

    [RightAttribute("11", "Allows adding new employees.")]
    [Description("Add Employee (Admin + HR)")]
    CanAddEmployee = 11,

    [RightAttribute("12", "Allows updating employee information.")]
    [Description("Update Employee (Admin + HR)")]
    CanUpdateEmployee = 12,

    #endregion

    #region Holidays

    [RightAttribute("13", "Allows viewing holidays.")]
    [Description("View Holiday (Admin + HR + Employee)")]
    CanViewHoliday = 13,

    [RightAttribute("14", "Allows viewing a holiday by ID.")]
    [Description("View Holiday By Id (Admin + HR)")]
    CanViewHolidayById = 14,

    [RightAttribute("15", "Allows adding holidays.")]
    [Description("Add Holiday (Admin + HR)")]
    CanAddHoliday = 15,

    [RightAttribute("16", "Allows editing holidays.")]
    [Description("Edit Holiday (Admin + HR)")]
    CanEditHoliday = 16,

    #endregion

    #region Leaves

    [RightAttribute("17", "Allows viewing leave records.")]
    [Description("View Leaves (Admin + HR)")]
    CanViewLeaves = 17,

    [RightAttribute("18", "Allows viewing leave by ID.")]
    [Description("View Leave By Id (Admin + HR)")]
    CanViewLeaveById = 18,

    [RightAttribute("19", "Allows adding leave requests.")]
    [Description("Add Leave (Admin + HR)")]
    CanAddLeave = 19,

    [RightAttribute("20", "Allows approving leave requests.")]
    [Description("Approve Leave (Admin + HR)")]
    CanApproveLeave = 20,

    #endregion

    #region Profile

    [RightAttribute("21", "Allows editing user profile.")]
    [Description("Edit Profile (Admin + HR + Employee)")]
    CanEditProfile = 21,

    [RightAttribute("22", "Allows viewing profile by ID.")]
    [Description("View Profile By Id (Admin + HR + Employee)")]
    CanViewProfileById = 22,

    #endregion

    #region Roles

    [RightAttribute("23", "Allows viewing roles.")]
    [Description("View Role (Admin + HR)")]
    CanViewRoles = 23,

    [RightAttribute("24", "Allows assigning roles.")]
    [Description("Assign Role (Admin + HR)")]
    CanAssignRole = 24,

    [RightAttribute("25", "Allows editing roles.")]
    [Description("Edit Role (Admin only)")]
    CanEditRole = 25,

    [RightAttribute("26", "Allows adding new roles.")]
    [Description("Add Role (Admin only)")]
    CanAddRole = 26,

    #endregion

    #region Users

    [RightAttribute("27", "Allows adding new users.")]
    [Description("Add User (Admin + HR)")]
    CanAddUser = 27,

    [RightAttribute("28", "Allows editing users.")]
    [Description("Edit User (Admin + HR)")]
    CanEditUser = 28,

    [RightAttribute("29", "Allows viewing users.")]
    [Description("View User (Admin + HR)")]
    CanViewUsers = 29,

    [RightAttribute("30", "Allows viewing users by ID.")]
    [Description("View User By Id (Admin + HR)")]
    CanViewUsersById = 30,

    #endregion

    #region Leads

    [RightAttribute("31", "Allows adding new leads.")]
    [Description("Add Lead (Admin + HR)")]
    CanAddLead = 31,

    [RightAttribute("32", "Allows editing leads.")]
    [Description("Edit Lead (Admin + HR)")]
    CanEditLead = 32,

    [RightAttribute("33", "Allows viewing leads.")]
    [Description("View Lead (Admin + HR)")]
    CanViewLeads = 33,

    [RightAttribute("34", "Allows viewing lead by ID.")]
    [Description("View Lead By Id (Admin + HR)")]
    CanViewLeadById = 34,

    #endregion

    #region Settings

    [RightAttribute("35", "Allows adding new settings.")]
    [Description("Add Settings (Admin + HR)")]
    CanAddSettings = 35,

    [RightAttribute("36", "Allows editing settings.")]
    [Description("Edit Settings (Admin + HR)")]
    CanEditSettings = 36,

    [RightAttribute("37", "Allows viewing settings.")]
    [Description("View Settings (Admin + HR)")]
    CanViewSettings = 37,

    [RightAttribute("38", "Allows viewing Settings by ID.")]
    [Description("View Settings By Id (Admin + HR)")]
    CanViewSettingsById = 38,

    #endregion

    #region Policies

    [RightAttribute("39", "Allows viewing Policies.")]
    [Description("View Policies (Admin + HR)")]
    CanViewAuthPolicies = 39,

    [RightAttribute("40", "Allows editing Policies.")]
    [Description("Edit Policies (Admin only)")]
    CanEditAuthPolicies = 40,

    #endregion
}
