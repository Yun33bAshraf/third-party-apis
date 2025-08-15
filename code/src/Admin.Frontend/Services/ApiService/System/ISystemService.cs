using IApply.Frontend.Models;
using IApply.Frontend.Models.Attendances;
using IApply.Frontend.Models.AuthPolicies.GetAuthPolicies;
using IApply.Frontend.Models.AuthPolicies.UpdateAuthPolicy;
using IApply.Frontend.Models.Cities;
using IApply.Frontend.Models.Employee;
using IApply.Frontend.Models.RequiredDocuments;
using IApply.Frontend.Models.Rights;
using IApply.Frontend.Models.Rights.GetRights;
using IApply.Frontend.Models.Roles;
using IApply.Frontend.Models.Roles.GetRoles;
using IApply.Frontend.Models.System.DeviceRegister;
using IApply.Frontend.Models.System.DeviceSetting;
using IApply.Frontend.Models.VehicleCatalog;
using IApply.Frontend.Models.VehicleTypes;

namespace IApply.Frontend.Services.ApiService.System;

public interface ISystemService
{
    Task<BaseResponseListing<GetAuthPoliciesResponse>?> AuthPolicyGet(int authpolicyId, int userTypeId, int pageNumber, int pageSize);
    Task<BaseResponse<AuthPolicyUpdateResponse>?> AuthPolicyUpdate(UpdateAuthPolicyRequest request);
    Task<GetRightsResponse?> GetSystemRights();
    //Task<BaseResponse?> DeviceRegister(string AccessToken, DeviceRegisterRequest request);
    Task<BaseResponse?> DeviceRegister(DeviceRegisterRequest request);
    Task<GetFileResponse?> GetFile(Guid FileId);
    Task<List<VehicleType>?> GetVehicleTypes();
    Task<BaseResponse?> AddUpdateVehicleType(VehicleType request);
    Task<List<VehicleCatalog>?> GetVehicleCatalog();
    Task<BaseResponse?> AddUpdateVehicleCatalog(VehicleCatalog request);
    Task<List<City>?> GetCities();
    Task<BaseResponse?> AddUpdateCity(City request);
    Task<List<RequiredDocument>?> GetRequiredDocuments();
    Task<BaseResponse?> AddUpdateRequiredDocuments(RequiredDocument request);
    Task<DeviceSetting> GetDeviceSetting();
    Task<BaseResponse?> AddUpdateDeviceSettings(DeviceSetting request);
    //Task<ListingBaseResponse<AttendenceMonthlySummary>?> GetAttendanceSummary(int? selectedMonth = null, int? selectedYear = null);

    //Task<ListingBaseResponse<Holidays?>?> GetHolidays(string? name, bool isWorkingDay);


    Task<BaseResponse?> ForgetPassword(string email);
    Task<BaseResponse?> ResetPassword(int userId, string password, string confirmPassword);
    Task<BaseResponse?> CompleteRegistration(int userId, string password, string retypePassword);
    Task<ListingBaseResponse<RoleModel?>?> GetRoles(string? Name, DateTime? startDate, DateTime? endDate, int pgNo, int pgSize);
    Task<ListingBaseResponse<RightsGet>?> GetRights();
    Task<ListingBaseResponse<CategoryGet>?> GetCategories();

    Task<BaseResponse?> RolesCreate(RolesCreateUpdateRequest request);

    Task<BaseResponse?> RolesUpdate(RolesCreateUpdateRequest request);


    Task<ListingBaseResponse<AttendenceDailySummary>?> GetAttendanceTodaySummary();


}
