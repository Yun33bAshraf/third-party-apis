using IApply.Frontend.Common.Enums;

namespace IApply.Frontend.Models.User.GetUsers
{
    public class File
    {
        public Guid Id { get; set; }
        public Guid RequiredDocumentId { get; set; }
        public string? FileName { get; set; }
        public string? ContentType { get; set; }
        public string? AdditionalInfo { get; set; }
        public FileType FileType { get; set; }
        public EntityType EntityType { get; set; }
        public DateTime? Expiry { get; set; }
        public DateTime UploadedAt { get; set; }
        public FileApprovalStatus FileApprovalStatus { get; set; }
        public string? BackendComments { get; set; }
        public DateTime? ActionTimestamp { get; set; }

    }
}
