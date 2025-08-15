using System.ComponentModel.DataAnnotations;
using IApply.Frontend.Common.CustomAttributes;

namespace IApply.Frontend.Models.Employee
{
    public class Employee
    {
        // Basic Employee Information
        [Display(Name = "Id")]
        [ColumnWidth("w-1")]
        public int EmployeeId { get; set; }

        [IgnoreInTable]
        public string EmployeeName { get; set; } = string.Empty;
        [Display(Name = "Employee Name")]

        public string DisplayEmployeeName => string.IsNullOrWhiteSpace(EmployeeName) ? "___" : EmployeeName;


        [IgnoreInTable]
        public string Email { get; set; } = string.Empty;
        [Display(Name = "Email")]

        public string DisplayEmail => string.IsNullOrWhiteSpace(Email) ? "___" : Email;


        // Address & Date of Birth
        [IgnoreInTable]
        public string Address { get; set; } = string.Empty;

        [IgnoreInTable]
        public DateOnly? DateOfBirth { get; set; }

        [Display(Name = "Date of Birth")]
        public string PurchaseDateFormat => DateOfBirth?.ToString("dd/MM/yyyy") ?? "___";

        // Gender & Designation
        [IgnoreInTable]
        public int GenderId { get; set; }

        [IgnoreInTable]
        public string? Gender { get; set; } = string.Empty;

        [IgnoreInTable]
        public string Designation { get; set; } = string.Empty;

        // Status & City Information
        [IgnoreInTable]
        public int? StatusId { get; set; }

        [IgnoreInTable]
        public string? Status { get; set; } = string.Empty;

        [Display(Name = "Status")]
        public string DisplayStatus => string.IsNullOrWhiteSpace(Status) ? "___" : Status;


        [IgnoreInTable]
        public int? CityId { get; set; }

        [IgnoreInTable]
        public string? City { get; set; } = string.Empty;


        [Display(Name = "City")]
        public string DisplayCity => string.IsNullOrWhiteSpace(City) ? "___" : City;

        // Date, Creation & User Information


        [IgnoreInTable]
        public int? UserId { get; set; }

        // Personal & Contact Information
        [IgnoreInTable]
        public string PersonalEmail { get; set; } = string.Empty;

        [IgnoreInTable]
        public string PermanentAddress { get; set; } = string.Empty;

        [IgnoreInTable]
        public string CurrentAddress { get; set; } = string.Empty;

        // Employment Dates
        [IgnoreInTable]
        public DateTime? DateOfJoining { get; set; }

        [Display(Name = "Date of Joining")]
        public string PurchaseDateFormatted => DateOfJoining?.ToString("dd/MM/yyyy") ?? "___";

        // Contact Numbers
        [IgnoreInTable]
        public string LandLineNumber { get; set; } = string.Empty;

        [IgnoreInTable]
        public string MobileNumber { get; set; } = string.Empty;

        [Display(Name = "Mobile Number")]
        public string DisplayMobileNumber => string.IsNullOrWhiteSpace(MobileNumber) ? "___" : MobileNumber;


        // Marital Status & CNIC Information
        [IgnoreInTable]
        public int? MaritalStatusId { get; set; }

        [IgnoreInTable]
        public string? MaritalStatus { get; set; } = string.Empty;

        [IgnoreInTable]
        public string? FatherHusbandName { get; set; } = string.Empty;

        [IgnoreInTable]
        public string? CNIC { get; set; } = string.Empty;

        [Display(Name = "CNIC")]
        public string DisplayCNIC => string.IsNullOrWhiteSpace(CNIC) ? "___" : CNIC;


        [IgnoreInTable]
        public string? CreatedAt { get; set; }
        [Display(Name = "Created Date")]

        public string DisplayCreatedAt => string.IsNullOrWhiteSpace(CreatedAt) ? "___" : CreatedAt;


        // Resignation & Last Working Date
        [IgnoreInTable]
        public DateTime? ResignDate { get; set; }

        [IgnoreInTable]
        public DateTime? LastWorkingDate { get; set; }

        // Audit Fields
        [IgnoreInTable]
        public string? UpdatedDate { get; set; }

        [Display(Name = "Updated Date")]
        public string DisplayUpdatedDate => string.IsNullOrWhiteSpace(UpdatedDate) ? "___" : UpdatedDate;


        [IgnoreInTable]
        public string? CreatedBy { get; set; } = string.Empty;

        [IgnoreInTable]
        public string? UpdatedBy { get; set; } = string.Empty;

        [IgnoreInTable]
        public string? Comment { get; set; }

        //public string DisplayMobileComment => string.IsNullOrEmpty(Comment) ? "___" : Comment;

        //public string? Comment { get; set; }

        [IgnoreInTable]
        public List<EmployeeFileGet> Files { get; set; } = [];
    }

    public class EmployeeFileGet
    {
        public int EmployeeFileId { get; set; }
        public int FileStoreId { get; set; }
        public int CategoryId { get; set; }
        public string FileUrl { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public string UploadedBy { get; set; } = string.Empty;
        public string? UploadedDate { get; set; } = string.Empty;
    }
}
