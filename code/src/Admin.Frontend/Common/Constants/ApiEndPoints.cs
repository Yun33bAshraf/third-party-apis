﻿namespace IApply.Frontend.Common.Constants;

public static class ApiEndpoints
{
    //// Base URLs
    //public const string BaseUrl = "https://api.example.com";
    //public const string Version = "/v1";

    // Authentication Endpoints
    public static class Auth
    {
        public const string Login = "Auth/login";
        public const string GetProfile = "Auth/get-profile";
        public const string ChangePassword = "Auth/change-password";
        public const string RefreshToken = "Auth/refresh-token";

        public const string CreateBackendUser = "/account/create-backend-user";
        public const string Configure2Factor = "/account/configure-authenticator";
        public const string Verify2Factor = "/account/verify-2f";
        public const string SendOtp = "/account/send-otp";
        public const string VerifyOtp = "/account/verify-otp";

        public const string ForgetPassword = "/account/forgot-password";
        public const string ResetPassword = "/account/reset-password";
        public const string CompleteRegistration = "/account/complete-registration";
    }

    public static class User
    {
        public const string UserGet = "User/get";
        public const string UserUpdate = "User/update";
        
        public const string MyRights = "/me/rights";
        public const string MyProfile = "/me/profile";
        public const string UpdateProfile = "/me/profile";
        public const string GetUsers = "/users";
        //public const string GetProfile = "/profile";
        public const string GetUserById = "/users/${userId}";
        public const string GetUserRoles = "/users/${userId}/roles";
        public const string CreateUserRoles = "/users/${userId}/roles";
        public const string DeleteUserRoles = "/users/${userId}/roles";
        public const string GetUserRights = "/users/${userId}/rights";
        public const string CreateUserRights = "/users/${userId}/rights";
        public const string DeleteUserRights = "/users/${userId}/rights";
        public const string UpdateUserStatus = "/users/${userId}";
        public const string UpdateFileStatus = "/users/${userId}/files/${fileId}";
        public const string UpdateUser = "/user-update";
    }

    public static class Role
    {
        public const string GetRoles = "/roles";
        public const string GetRoleById = "/roles/${roleId}";
        public const string AddRole = "/roles";
        public const string UpdateRole = "/roles";
        public const string DeleteRole = "/roles/${roleId}";
        public const string GetRoleRights = "/roles/${roleId}/rights";
        public const string CreateRoleRights = "/roles/${roleId}/rights";
        public const string DeleteRoleRights = "/roles/${roleId}/rights";

        public const string RolesGet = "/roles";
        public const string RightsGet = "/get-rights";
        public const string RolesCreate = "/roles";
         public const string RolesUpdate = "/roles";
    }

    public static class System
    {
        public const string GetAuthPolicies = "System/get-auth-policy";
        public const string UpdateAuthPolicy = "System/update-auth-policy";

        public const string Get = "/get-category";
        public const string EntityGet = "/get-entity-type";
        public const string CategoryCreate = "/category";
        public const string GetMonthYear = "/get-month-year";
        public const string GetSystemRights = "/system/system-rights";
        public const string DeviceRegister = "/system/device-register";
        public const string GetFile = "/system/file/${fileId}";
        public const string GetVehicleTypes = "/system/vehicle-types";
        public const string AddUpdateVehicleType = "/system/vehicle-types";
        public const string GetVehicleCatalog = "/system/vehicle-catalog";
        public const string AddUpdateVehicleCatalog = "/system/vehicle-catalog";
        public const string GetCities = "/system/cities";
        public const string AddUpdateCity = "/system/cities";
        public const string GetRequiredDocuments = "/system/required-documents";
        public const string AddUpdateRequiredDocuments = "/system/required-documents";
        public const string GetRidePlans = "/system/ride-plans";
        public const string AddUpdateRidePlans = "/system/ride-plans";
        public const string GetDeviceSettings = "/system/device-settings";
        public const string AddUpdateDeviceSettings = "/system/device-settings";
    }

    public static class Me
    {
        public const string GetMyRights = "/me/rights";
        public const string FindDrivers = "/me/find-drivers";
    }
    
    public static class Assets
    {
        public const string GetAssets = "/assets";
        public const string PostAssets = "/assets";
        public const string UpdateAssets = "/assets";
        public const string AssetFileUpload = "/assets/upload";
    }
    
    public static class Attendances
    {
        public const string GetAttendances = "/attendance";
        public const string GetAttendanceSummary = "/get-monthly-summary";
        public const string GetAttendanceDailySummary = "/get-summary";
        public const string GetAttendanceTodaySummary = "/get-today-summary";
    }

    public static class Business
    {
        public const string List = "Business/list";
        public const string GetAllCurrency = "Business/get-all-currency";
        public const string CreateUpdateBusiness = "Business/create-update-business-details";

    }

    public static class Subscription
    {
        public const string List = "Subscription/list";
        public const string GetSubscribedBusiness = "Subscription/get-subscribed-business";
        public const string CreateUpdateSubscription = "Subscription/create-update-subscription";
        public const string CreateUpdateBusinessSubscription = "Subscription/create-update-business-subscription";

    }
    public static class Holidays
    {
        public const string GetHolidays = "/holiday";
        public const string CreateHolidays = "/holiday";
        public const string UpdateHolidays = "/holiday-update";
    }

    public static class Employees
    {
        public const string GetEmployees = "/employee";
        public const string CreateEmployees = "/employee";
        public const string UpdateEmployees = "/employee-update";
        //public const string GetEmployeeById = "/employees/${employeeId}";
        //public const string AddEmployee = "/employees";
        //public const string UpdateEmployee = "/employees";
        //public const string DeleteEmployee = "/employees/${employeeId}";
        public const string EmployeeFileUpload = "/employee/upload";
    }

    public static class EmployeeLeave
    {
        public const string GetEmployeeLeaves = "/employee-leave";
        public const string EmpLeaveCreate = "/employee-leave";
        public const string EmpLeaveUpdate = "/update";
    }
    public static class Lead
    {
        public const string GetLeads = "/lead";
        public const string LeadCreate = "/lead";
        public const string LeadUpdate = "/lead";
    }
    
    public static class ERPUsers
    {
        public const string GetERPUsers = "/user";
    }

    public static class Workspace
    {
        public const string UserWorkspaceGet = "Workspace/user";
    }

    public static class Project
    {
        public const string GetProject = "Project/get";
        public const string GetUserProject = "Project/user";
    }
}

