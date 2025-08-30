using System.ComponentModel;

namespace OpenProfileAPI.Frontend.Common.Enums;

public enum EmployeeFilesEnum
{
    [Description("SSC/O-Levels (Certificate)")]
    Document1 = 1,
    [Description("HSSC/A-Level (Certificate)")]
    Document2 = 2,
    [Description("Last Degree (Transcript/Certificate)")]
    LastDegree = 3,
    [Description("CV/Resume")]
    CV = 4,
    [Description("CNIC Front Side")]
    CNICFront = 5,
    [Description("Previous Experience Letter (If any)")]
    ExperienceLetter = 6,
    [Description("CNIC Back Side")]
    CNICBack = 7,

    //ProfilePicture,
    //Resume,
    //OfferLetter,
    //AppointmentLetter,
    //Contract,
    //IDProof,
    //AddressProof,
    //ExperienceCertificate,
    //SalarySlip,
    //TaxDocuments,
    //BankDetails,
    //AttendanceReport,
    //LeaveApplications,
    //PerformanceReview,
    //TrainingCertificates,
    //MedicalRecords,
    //TerminationLetter,
    //WarningLetter,
    //ReferenceLetter,
    //PromotionLetter,
    //BonusDetails,
    //Other
}