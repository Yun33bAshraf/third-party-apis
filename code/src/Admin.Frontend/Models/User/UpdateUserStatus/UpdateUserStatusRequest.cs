using IApply.Frontend.Common.Enums;

namespace IApply.Frontend.Models.User.UpdateUserStatus
{
    public class UpdateUserStatusRequest
    {
        public BackendActivationStatus BackendActivationStatus { get; set; }
        public string BackendComments { get; set; } = string.Empty;
    }
}
