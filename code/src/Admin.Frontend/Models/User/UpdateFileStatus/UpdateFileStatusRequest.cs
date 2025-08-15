using IApply.Frontend.Common.Enums;

namespace IApply.Frontend.Models.User.UpdateFileStatus
{
    public class UpdateFileStatusRequest
    {
        public FileApprovalStatus FileApprovalStatus { get; set; }
        public string BackendComments { get; set; } = string.Empty;
    }
}
