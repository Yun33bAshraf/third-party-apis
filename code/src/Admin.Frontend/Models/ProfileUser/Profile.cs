using IApply.Frontend.Models;

namespace IApply.Frontend.Models.ProfileUser
{
    public class ProfileResponse
    {
        public bool IsSuccess { get; set; } = false;
        public int ErrorCode { get; set; }
        public Profile? Data { get; set; }
        public Pagination? Pagination { get; set; }
    }
    public class Profile
    {
        public Roles? UserRole {  get; set; }
    }
    public class Roles
    {
        public int RoleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Rights> UserRights { get; set; } = new List<Rights>();

    }
    public class Rights
    {
        public int RightId { get; set; }
        public string Name { get; set; }= string.Empty;
        public string Description { get; set; }= string.Empty;
    }
}
