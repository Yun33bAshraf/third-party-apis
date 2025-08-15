namespace IApply.Frontend.Models.User.ERPUsers.CompleteRegistration
{
    public class CompleteRegistrationRequest
    {
        public int Id { get; set; }
        public string Password { get; set; } = string.Empty;
        public string RetypePassword { get; set; } = string.Empty;
    }
}
