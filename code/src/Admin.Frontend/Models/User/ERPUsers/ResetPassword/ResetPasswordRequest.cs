namespace IApply.Frontend.Models.User.ERPUsers.ResetPassword;

public class ResetPasswordRequest
{
    public int UserId { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
