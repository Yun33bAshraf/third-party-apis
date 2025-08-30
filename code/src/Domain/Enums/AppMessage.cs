using System.ComponentModel;

namespace OpenProfileAPI.Domain.Enums;

public enum AppMessage
{
    #region Generic Codes

    [Description("Record created successfully")]
    RecordCreatedSuccessfully,

    [Description("Record updated successfully")]
    RecordUpdatedSuccessfully,

    [Description("Record deleted successfully")]
    RecordDeletedSuccessfully,

    [Description("Unable to update record")]
    UnableToUpdateRecord,

    [Description("Unable to delete record")]
    UnableToDeleteRecord,

    [Description("Unable to create record")]
    UnableToCreateRecord,

    [Description("Unable to find record")]
    UnableToFindRecord,

    [Description("Unable to find record for update")]
    UnableToFindRecordForUpdate,

    #endregion Generic Codes

    [Description("User with same email already exist")]
    UserWithSameEmailAlreadyExist,

    [Description("Data Fetched Successfully.")]
    DataFetchedSuccessfully,

    [Description("No date found.")]
    NoDataFound,

    [Description("Unauthorized Access.")]
    UnauthorizedAccess,

    #region User

    [Description("User not found")]
    UserNotFound,

    [Description("Invalid Password")]
    InvalidPassword,


    [Description("Account created successfully. Please check your email.")]
    AccountCreatedSuccessfully,

    [Description("Failed to register user")]
    FailedToRegisterUser,

    [Description("Email or Password is incorrect")]
    EmailOrPasswordIsIncorrect,

    [Description("Password updated successfully")]
    PasswordUpdatedSuccessfully,

    [Description("Email is incorrect")]
    EmailIsIncorrect,

    [Description("Password Reset Email Sent. Please Check Your Email.")]
    PasswordResetEmailSent,

    [Description("Account already exists.")]
    AccountAlreadyExists,

    [Description("Email already confirmed.")]
    EmailAlreadyConfirmed,

    [Description("Invalid or expired token.")]
    InvalidOrExpiredToken,

    [Description("Email successfully verified.")]
    EmailVerificationSuccessful,

    [Description("User Profile Not Found.")]
    UserProfileNotFound,

    [Description("Profile Updated Successfully.")]
    ProfileUpdatedSuccessfully,

    [Description("User-defined keyword added successfully.")]
    UserKeywordsCreated,

    #endregion User

    #region PASSWORD RESET

    [Description("Password Reset Successfully")]
    PasswordResetSuccessfully,
    
    [Description("Password Reset Failed")]
    PasswordResetFailed,

    [Description("Passwords cannot be empty")]
    PasswordCannotBeEmpty,

    [Description("Old password and new password cannot be the same")]
    PasswordCannotBeSame,

    [Description("New password does not meet the required strength. Use at least 8 characters, including upper and lower case letters, numbers, and special symbols.")]
    PasswordStrengthWeak,

    [Description("Failed to update the password.")]
    PasswordUpdateFailed,

    [Description("Unable to register user.")]
    UnableToRegisterUser,
    
    [Description("Registration Email Sent Successfully.")]
    RegistrationEmailSentSuccessfully,

    [Description("Registration Completed Successfully.")]
    RegistrationCompletedSuccessfully,

    #endregion PASSWORD RESET

    [Description("Comment Updated")]
    UpdateComment,

    #region AUTH

    [Description("Login Successful")]
    LoginSuccessful,
    
    [Description("Account is locked due to multiple failed login attempts. Please try again later.")]
    AccountAlreadyLocked,

    [Description("Refresh Token Not Found")]
    RefreshTokenNotFound,

    [Description("Auth Policy Token Not Found")]
    AuthPolicyTokenNotFound,

    #endregion AUTH

    #region PROJECT

    [Description("Project not found.")]
    ProjectNotFound,

    #endregion PROJECT

    #region FILE UPLOAD

    [Description("File Upload Successful.")]
    FileUploadSuccessful,

    [Description("File Upload Failed.")]
    FileUploadFailed,

    #endregion FILE UPLOAD

    #region TICKET

    [Description("Ticket not found.")]
    TicketNotFound,

    [Description("Ticket Assignment Updated")]
    TicketAssignmentUpdated,

    #endregion TICKET

    #region EXPERIENCE

    [Description("Experience details saved.")]
    ExperienceSaved,

    #endregion EXPERIENCE

    #region EDUCATION

    [Description("Education details saved.")]
    EducationSaved,

    #endregion EDUCATION

    #region SKILL

    [Description("Skills updated.")]
    SkillsSaved,

    #endregion SKILL

    #region SUBSCRIPTION

    [Description("Subscription Plan Not Found.")]
    SubscriptionPlanNotFound,

    [Description("Subscription started successfully.")]
    SubscriptionStartedSuccessfully,    
    
    [Description("Subscription canceled successfully.")]
    SubscriptionCanceledSuccessfully,

    [Description("No active subscription found.")]
    NoActiveSubscriptionFound,
    
    [Description("Subscription Plan Changed Successfully.")]
    SubscriptionPlanChangedSuccessfully,

    #endregion SUBSCRIPTION

    #region STRIPE

    [Description("Invalid Stripe API Key.")]
    StripeAPIKeyInvalid,

    #endregion STRIPE
}
