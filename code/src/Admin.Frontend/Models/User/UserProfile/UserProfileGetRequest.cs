namespace IApply.Frontend.Models.User.UserProfile;

public class UserProfileGetRequest
{
    public int UserId { get; set; }
    public string? Name { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
