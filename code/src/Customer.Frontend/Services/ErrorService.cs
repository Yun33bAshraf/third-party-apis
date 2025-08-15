namespace IApply.Frontend.Services;

public static class ErrorService
{
    public const int UnknownError = 10;
    public const int InvalidUserNameOrPassword = 101;
    public const int UserNotFound = 102;
    public const int UserLocked = 103;
    public const int InvalidVerificationCode = 201;
    public const int ExpiredRequest = 202;
    public const int InvalidRequest = 203;
    public const int RequestNotFound = 204;
    public const int TFAlreadyConfigured = 301;
    public const int TFAlreadyVerified = 302;
    public const int OldPasswordMismatch = 401;
    public const int EmailNotFound = 501;
    public const int MobileNoNotFound = 502;
    public const int AssociatedRecords = 601;
    public const int FirebaseError = 800;

    public static string GetErrorMessage(int errorCode)
    {
        return errorCode switch
        {
            UnknownError => "An unknown error occurred. Please try again later.",
            InvalidUserNameOrPassword => "Invalid username or password. Please try again.",
            UserNotFound => "User not found. Please check your credentials.",
            UserLocked => "Your account is locked. Please contact support.",
            InvalidVerificationCode => "Invalid verification code. Please check and try again.",
            ExpiredRequest => "The request has expired. Please try logging in again.",
            InvalidRequest => "The request is invalid. Please check your inputs.",
            RequestNotFound => "The login request could not be found.",
            TFAlreadyConfigured => "Two-factor authentication is already configured.",
            TFAlreadyVerified => "Two-factor authentication is already verified.",
            OldPasswordMismatch => "The old password does not match. Please try again.",
            EmailNotFound => "Email address not found. Please check and try again.",
            MobileNoNotFound => "Mobile number not found. Please check and try again.",
            AssociatedRecords => "Unable to proceed due to associated records.",
            FirebaseError => "An error occurred with Firebase.",
            _ => "An unknown error occurred. Please try again later."
        };
    }

}
