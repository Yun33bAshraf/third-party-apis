using System.ComponentModel.DataAnnotations;
using IApply.Frontend.Common.CustomAttributes;

namespace IApply.Frontend.Models.Lead
{
    public class Lead
    {
        [Display(Name = "Id")]
        [ColumnWidth("w-1")]
        public int LeadId { get; set; }

        [Display(Name = "Lead Name")]
        public string FullName { get; set; } = string.Empty;

        [Display(Name = "Email")]
        public string? Email { get; set; } = string.Empty;

        [IgnoreInTable]
        public string? PhoneNumber { get; set; } = string.Empty;

        [IgnoreInTable]
        public string? CompanyName { get; set; } = string.Empty;

        [IgnoreInTable]
        public string? JobTitleRole { get; set; } = string.Empty;

        [IgnoreInTable]
        [Display(Name = "LeadSourceId")]
        public int LeadSourceId { get; set; }

        [Display(Name = "Lead Source")]
        public string LeadSource { get; set; } = string.Empty;

        [IgnoreInTable]
        [Display(Name = "LeadStatusId")]
        public int LeadStatusId { get; set; }

        [Display(Name = "Lead Status")]
        public string LeadStatus { get; set; } = string.Empty;

        [IgnoreInTable]
        [Display(Name = "IndustryId")]
        public int IndustryId { get; set; }

        [Display(Name = "Industry")]
        public string Industry { get; set; } = string.Empty;

        [IgnoreInTable]
        [Display(Name = "Comment")]
        public string? Comment { get; set; } = string.Empty;

        [Display(Name = "Created Date")]
        public string CreatedAt { get; set; }

        [IgnoreInTable]
        [Display(Name = "Created At Utc")]
        public DateTime CreatedAtUtc { get; set; }

        [Display(Name = "Updated Date")]
        public string? UpdatedAt { get; set; }

        [IgnoreInTable]
        [Display(Name = "Updated A tUtc")]
        public DateTime UpdatedAtUtc { get; set; }
    }
}